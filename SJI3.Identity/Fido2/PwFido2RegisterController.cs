﻿using System.Text;
using Fido2NetLib;
using Fido2NetLib.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SJI3.Identity.Data;

namespace Fido2Identity;

[Route("api/[controller]")]
public class PwFido2RegisterController : Controller
{
    private readonly Fido2 _lib;
    private readonly Fido2Store _fido2Store;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOptions<Fido2Configuration> _optionsFido2Configuration;


    public PwFido2RegisterController(
        Fido2Store fido2Store,
        UserManager<ApplicationUser> userManager,
        IOptions<Fido2Configuration> optionsFido2Configuration)
    {
        _userManager = userManager;
        _optionsFido2Configuration = optionsFido2Configuration;
        _fido2Store = fido2Store;

        _lib = new Fido2(new Fido2Configuration
        {
            ServerDomain = _optionsFido2Configuration.Value.ServerDomain,
            ServerName = _optionsFido2Configuration.Value.ServerName,
            Origins = _optionsFido2Configuration.Value.Origins,
            TimestampDriftTolerance = _optionsFido2Configuration.Value.TimestampDriftTolerance
        });
    }

    private static string FormatException(Exception e)
    {
        return $"{e.Message}{(e.InnerException != null ? " (" + e.InnerException.Message + ")" : "")}";
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("/pwmakeCredentialOptions")]
    public async Task<JsonResult> MakeCredentialOptions([FromForm] string username, [FromForm] string displayName, [FromForm] string attType, [FromForm] string authType, [FromForm] bool requireResidentKey, [FromForm] string userVerification)
    {
        try
        {
            if (string.IsNullOrEmpty(username))
            {
                username = $"{displayName} (Nameless user created at {DateTime.UtcNow})";
            }

            var user = new Fido2User
            {
                DisplayName = displayName,
                Name = username,
                Id = Encoding.UTF8.GetBytes(username)
            };
            
            var items = await _fido2Store.GetCredentialsByUserNameAsync(username);
            var existingKeys = new List<PublicKeyCredentialDescriptor>();
            foreach (var publicKeyCredentialDescriptor in items)
            {
                if (publicKeyCredentialDescriptor.Descriptor != null)
                    existingKeys.Add(publicKeyCredentialDescriptor.Descriptor);
            }
            
            var authenticatorSelection = new AuthenticatorSelection
            {
                RequireResidentKey = requireResidentKey,
                UserVerification = userVerification.ToEnum<UserVerificationRequirement>()
            };

            if (!string.IsNullOrEmpty(authType))
                authenticatorSelection.AuthenticatorAttachment = authType.ToEnum<AuthenticatorAttachment>();

            var extensions = new AuthenticationExtensionsClientInputs
            {
                Extensions = true,
                UserVerificationMethod = true,
            };

            var options = _lib.RequestNewCredential(user, existingKeys, authenticatorSelection, attType.ToEnum<AttestationConveyancePreference>(), extensions);
            
            HttpContext.Session.SetString("fido2.attestationOptions", options.ToJson());
            
            return Json(options);
        }
        catch (Exception e)
        {
            return Json(new CredentialCreateOptions { Status = "error", ErrorMessage = FormatException(e) });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("/pwmakeCredential")]
    public async Task<JsonResult> MakeCredential([FromBody] AuthenticatorAttestationRawResponse attestationResponse)
    {
        try
        {
            var jsonOptions = HttpContext.Session.GetString("fido2.attestationOptions");
            var options = CredentialCreateOptions.FromJson(jsonOptions);

            async Task<bool> Callback(IsCredentialIdUniqueToUserParams args, CancellationToken cancellationToken)
            {
                var users = await _fido2Store.GetUsersByCredentialIdAsync(args.CredentialId);
                if (users.Count > 0) return false;

                return true;
            }

            var success = await _lib.MakeNewCredentialAsync(attestationResponse, options, Callback);

            if (success.Result != null)
            {
                await _fido2Store.AddCredentialToUserAsync(options.User, new FidoStoredCredential
                {
                    UserName = options.User.Name,
                    Descriptor = new PublicKeyCredentialDescriptor(success.Result.CredentialId),
                    PublicKey = success.Result.PublicKey,
                    UserHandle = success.Result.User.Id,
                    SignatureCounter = success.Result.Counter,
                    CredType = success.Result.CredType,
                    RegDate = DateTime.Now,
                    AaGuid = success.Result.Aaguid
                });
            }
            

            var user = await CreateUser(options.User.Name);
            // await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Json(new Fido2.CredentialMakeResult("error",
                    $"Unable to load user with ID '{_userManager.GetUserId(User)}'.",
                    success.Result));
            }

            //await _userManager.SetTwoFactorEnabledAsync(user, true);
            //var userId = await _userManager.FindByNameAsync(user);

            return Json(success);
        }
        catch (Exception e)
        {
            return Json(new Fido2.CredentialMakeResult("error", FormatException(e), null));
        }
    }

    private async Task<ApplicationUser> CreateUser(string userEmail)
    {
        var user = new ApplicationUser { UserName = userEmail, Email = userEmail, EmailConfirmed = true };
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            //await _signInManager.SignInAsync(user, isPersistent: false);
        }

        return user;
    }
}