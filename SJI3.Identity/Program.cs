using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Security.Cryptography;
using Fido2NetLib;
using SJI3.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SJI3.Identity.Data;
using SJI3.Identity.Fido2;
using static OpenIddict.Abstractions.OpenIddictConstants;
using Fido2Identity;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
    {
        options
            .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            .EnableSensitiveDataLogging();

        options.UseOpenIddict();
    });

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddTokenProvider<Fido2UserTwoFactorTokenProvider>("FIDO2");

builder.Services.Configure<Fido2Configuration>(builder.Configuration.GetSection("fido2"));
builder.Services.AddScoped<Fido2Store>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.ClaimsIdentity.UserNameClaimType = Claims.Name;
    options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
    options.ClaimsIdentity.RoleClaimType = Claims.Role;
    options.ClaimsIdentity.EmailClaimType = Claims.Email;

    // Note: to require account confirmation before login,
    // register an email sender service (IEmailSender) and
    // set options.SignIn.RequireConfirmedAccount to true.
    //
    // For more information, visit https://aka.ms/aspaccountconf.
    options.SignIn.RequireConfirmedAccount = false;
});

builder.Services.AddQuartz(options =>
{
    options.UseMicrosoftDependencyInjectionJobFactory();
    options.UseSimpleTypeLoader();
    options.UseInMemoryStore();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policyBuilder =>
        {
            policyBuilder
                .AllowCredentials()
                .WithOrigins("https://localhost:4200")
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect("KeyCloak", "KeyCloak", options =>
    {
        options.SignInScheme = "Identity.External";
        options.Authority = builder.Configuration.GetSection("Keycloak")["ServerRealm"];
        options.ClientId = builder.Configuration.GetSection("Keycloak")["ClientId"];
        options.ClientSecret = builder.Configuration.GetSection("Keycloak")["ClientSecret"];
        options.MetadataAddress = builder.Configuration.GetSection("Keycloak")["Metadata"];

        options.GetClaimsFromUserInfoEndpoint = true;
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.SaveTokens = true;
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.RequireHttpsMetadata = false; //dev

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name",
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuer = true
        };
    });

builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
       options.UseEntityFrameworkCore()
            .UseDbContext<ApplicationDbContext>();

        options.UseQuartz();
    })
    .AddServer(options =>
    {
        options.DisableAccessTokenEncryption(); // dev
        options.Configure(opt => opt.CodeChallengeMethods.Add(CodeChallengeMethods.Plain)); //dev

        options.SetAuthorizationEndpointUris("/connect/authorize")
            .SetLogoutEndpointUris("/connect/logout")
            .SetIntrospectionEndpointUris("/connect/introspect")
            .SetTokenEndpointUris("/connect/token")
            .SetUserinfoEndpointUris("/connect/userinfo")
            .SetVerificationEndpointUris("/connect/verify");

        options.AllowAuthorizationCodeFlow()
            .AllowHybridFlow()
            .AllowClientCredentialsFlow()
            .AllowRefreshTokenFlow();

        options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles, "sji3_api");

        //options.AddDevelopmentEncryptionCertificate()
        //    .AddDevelopmentSigningCertificate(); // for RS256

        options.AddEncryptionKey(new SymmetricSecurityKey(Convert.FromBase64String("YmNjMzUwOTljZGYyNDk2MmEzZTI1ZjI0ZjI5YTllMDI=")))
            .AddSigningKey(new ECDsaSecurityKey(ECDsa.Create(ECCurve.NamedCurves.nistP256))); // ES256

        options.UseAspNetCore()
            .EnableAuthorizationEndpointPassthrough()
            .EnableLogoutEndpointPassthrough()
            .EnableTokenEndpointPassthrough()
            .EnableUserinfoEndpointPassthrough()
            .EnableStatusCodePagesIntegration();
    })
    .AddValidation(options =>
    {
        options.UseLocalServer();
        options.UseAspNetCore();
    });

builder.Services.AddHostedService<SeedBackgroundService>();

builder.Services.AddMassTransit(cfg =>
{
    cfg.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        configurator.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseStatusCodePagesWithReExecute("~/error");
    //app.UseExceptionHandler("~/error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});

app.Run();
