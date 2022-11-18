
using System.Text;
using Fido2NetLib.Objects;
using Fido2NetLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SJI3.Identity.Data;

namespace Fido2Identity;

[Route("api/[controller]")]
public class PwFido2SignInController : Controller
{
    private readonly Fido2 _lib;
    private readonly Fido2Store _fido2Store;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IOptions<Fido2Configuration> _optionsFido2Configuration;

    public PwFido2SignInController(
        Fido2Store fido2Store,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<Fido2Configuration> optionsFido2Configuration)
    {
        _userManager = userManager;
        _optionsFido2Configuration = optionsFido2Configuration;
        _signInManager = signInManager;
        _userManager = userManager;
        _fido2Store = fido2Store;

        _lib = new Fido2(new Fido2Configuration()
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
    [Route("/pwassertionOptions")]
    public async Task<ActionResult> AssertionOptionsPost([FromForm] string username, [FromForm] string userVerification)
    {
        try
        {
            var existingCredentials = new List<PublicKeyCredentialDescriptor>();

            if (!string.IsNullOrEmpty(username))
            {
                var applicationUser = await _userManager.FindByNameAsync(username);
                var user = new Fido2User
                {
                    DisplayName = applicationUser.UserName,
                    Name = applicationUser.UserName,
                    Id = Encoding.UTF8.GetBytes(applicationUser.UserName)
                };

                if (user == null) throw new ArgumentException("Username was not registered");
                
                var items = await _fido2Store.GetCredentialsByUserNameAsync(applicationUser.UserName);
                existingCredentials = items.Select(c => c.Descriptor).NotNull().ToList();
            }

            var extensions = new AuthenticationExtensionsClientInputs
            {
                UserVerificationMethod = true,
            };
            
            var verificationRequirement = string.IsNullOrEmpty(userVerification) ? UserVerificationRequirement.Discouraged : userVerification.ToEnum<UserVerificationRequirement>();
            
            var options = _lib.GetAssertionOptions(
                existingCredentials,
                verificationRequirement,
                extensions
            );
            
            HttpContext.Session.SetString("fido2.assertionOptions", options.ToJson());
            
            return Json(options);
        }

        catch (Exception e)
        {
            return Json(new AssertionOptions { Status = "error", ErrorMessage = FormatException(e) });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("/pwmakeAssertion")]
    public async Task<JsonResult> MakeAssertion([FromBody] AuthenticatorAssertionRawResponse clientResponse)
    {
        try
        {
            var jsonOptions = HttpContext.Session.GetString("fido2.assertionOptions");
            var options = AssertionOptions.FromJson(jsonOptions);
            
            var credential = await _fido2Store.GetCredentialByIdAsync(clientResponse.Id);

            if (credential == null)
            {
                throw new Exception("Unknown credentials");
            }
            
            var storedCounter = credential.SignatureCounter;
            
            async Task<bool> Callback(IsUserHandleOwnerOfCredentialIdParams args, CancellationToken cancellationToken)
            {
                var credentials = await _fido2Store.GetCredentialsByUserHandleAsync(args.UserHandle);
                return credentials.Any(c => c.Descriptor != null && c.Descriptor.Id.SequenceEqual(args.CredentialId));
            }

            if (credential.PublicKey == null)
            {
                throw new InvalidOperationException($"No public key");
            }
            
            var res = await _lib.MakeAssertionAsync(
                clientResponse, options, credential.PublicKey, storedCounter, Callback);
            
            await _fido2Store.UpdateCounterAsync(res.CredentialId, res.Counter);

            var applicationUser = await _userManager.FindByNameAsync(credential.UserName);
            if (applicationUser == null)
            {
                throw new InvalidOperationException($"Unable to load user.");
            }

            await _signInManager.SignInAsync(applicationUser, isPersistent: false);
            
            return Json(res);
        }
        catch (Exception e)
        {
            return Json(new AssertionVerificationResult { Status = "error", ErrorMessage = FormatException(e) });
        }
    }
}