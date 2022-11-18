using OpenIddict.Abstractions;
using SJI3.Identity.Data;
using static OpenIddict.Abstractions.OpenIddictConstants;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace SJI3.Identity;

public class SeedBackgroundService : BackgroundService
{
    private readonly ILogger<SeedBackgroundService> _logger;
    private readonly IServiceProvider _provider;

    public SeedBackgroundService(ILogger<SeedBackgroundService> logger, IServiceProvider provider)
    {
        _logger = logger;
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Seed Hosted Service is running");

        await BackgroundProcessing(stoppingToken);
    }

    private async Task BackgroundProcessing(CancellationToken cancellationToken)
    {
        using var scope = _provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        // Seed Application Clients
        var applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        
        if (await applicationManager.FindByClientIdAsync("sji3_ui_resource_server", cancellationToken) is null)
        {
            await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "sji3_ui_resource_server",
                ConsentType = ConsentTypes.Explicit,
                DisplayName = "sji3_ui client PKCE",
                DisplayNames =
                        {
                            [CultureInfo.GetCultureInfo("en-US")] = "SJI3 UI"
                        },
                PostLogoutRedirectUris =
                        {
                            new Uri("https://localhost:4200")
                        },
                RedirectUris =
                        {
                            new Uri("https://localhost:4200")
                        },
                Permissions =
                        {
                            Permissions.Endpoints.Authorization,
                            Permissions.Endpoints.Logout,
                            Permissions.Endpoints.Token,
                            Permissions.Endpoints.Revocation,
                            Permissions.GrantTypes.AuthorizationCode,
                            Permissions.GrantTypes.RefreshToken,
                            Permissions.ResponseTypes.Code,
                            Permissions.Scopes.Email,
                            Permissions.Scopes.Profile,
                            Permissions.Scopes.Roles,
                            Permissions.Prefixes.Scope + "sji3_api"
                        },
                Requirements =
                        {
                            Requirements.Features.ProofKeyForCodeExchange
                        }
            }, cancellationToken);
        }

        if (await applicationManager.FindByClientIdAsync("sji3_api_resource_server", cancellationToken) is null)
        {
            var descriptor = new OpenIddictApplicationDescriptor
            {
                ClientId = "sji3_api_resource_server",
                ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
                Permissions =
                {
                    Permissions.Endpoints.Introspection,
                    Permissions.Prefixes.Scope + "sji3_api"
                }
            };

            await applicationManager.CreateAsync(descriptor, cancellationToken);
        }

        if (await applicationManager.FindByClientIdAsync("postman", cancellationToken) is null)
        {
            await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "postman",
                DisplayName = "Postman",
                ClientSecret = "388D45FA-B36B-4988-BA59-B187D329C207",
                RedirectUris = { new Uri("https://www.getpostman.com/oauth2/callback") },
                Permissions =
                {
                   Permissions.Endpoints.Authorization,
                   Permissions.Endpoints.Device,
                   Permissions.Endpoints.Token,
                   Permissions.GrantTypes.AuthorizationCode,
                   Permissions.GrantTypes.DeviceCode,
                   Permissions.GrantTypes.Password,
                   Permissions.GrantTypes.RefreshToken,
                   Permissions.Scopes.Email,
                   Permissions.Scopes.Profile,
                   Permissions.Scopes.Roles,
                   Permissions.Prefixes.Scope + "sji3_api",
                   Permissions.ResponseTypes.Code
                }
            }, cancellationToken);
        }

        // Seed Application Scopes

        var scopeManager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

        if (await scopeManager.FindByNameAsync("sji3_api", cancellationToken) is null)
        {
            await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                DisplayName = "SJI3 API Access",
                DisplayNames =
                {
                    [CultureInfo.GetCultureInfo("en-US")] = "SJI3 API Access"
                },
                Name = "sji3_api",
                Resources =
                {
                    "sji3_api_resource_server"
                }
            }, cancellationToken);
        }

        // Seed Admin User
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!userManager.Users.Any())
        {
            var user = new ApplicationUser
            {
                Id = new Guid("a1c4b914-c059-46f5-84bf-0415c118c39e").ToString(),
                Email = "admin@sji3.local",
                UserName = "admin@sji3.local"
            };

            await userManager.CreateAsync(user, "@Admin123");
            await userManager.AddToRoleAsync(user, "Admin");
            await userManager.UpdateAsync(user);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Seed Hosted Service is stopping");

        await base.StopAsync(cancellationToken);
    }
}