using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using SJI3.API.Common;
using SJI3.Infrastructure.Data;
using SJI3.Infrastructure.Logging;
using Refit;
using Serilog;
using Serilog.Events;
using System.Text.Json;
using FluentValidation;
using FluentValidation.AspNetCore;
using SJI3.Core.Common.Domain;
using SJI3.Core.Common.Infra;
using SJI3.Core.Services.Domain;
using SJI3.Infrastructure.AntiCorruption.Domain;
using SJI3.Infrastructure.AntiCorruption.HostedServices;
using SJI3.Infrastructure.AntiCorruption.HttpClients;
using SJI3.Infrastructure.Services.Notification;
using SJI3.Infrastructure.Services.Queue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using SJI3.Infrastructure.AntiCorruption.WCFClients.Common;
using SJI3.Infrastructure.AntiCorruption.WCFClients.Services;
using SJI3.Infrastructure.Consumers.AMQP.ApplicationUser.ApplicationUserCreated;
using SJI3.Infrastructure.Consumers.InMemory.Infra;
using SJI3.Infrastructure.Consumers.InMemory.TaskUnit.TaskStatusChanged;
using SJI3.Infrastructure.Consumers.InMemory.TaskUnit.TaskUnitAdded;
using SJI3.Infrastructure.Extensions;

const string allowAllOrigins = "AllowAllOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel();
builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());
builder.WebHost.UseIISIntegration();

builder.Host.UseSerilog((_, conf) =>
{
    conf
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.CustomSchemaIds(x => x.FullName);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowAllOrigins, policy =>
    {

        policy.AllowCredentials()
            .WithOrigins("https://localhost:4200")
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// var guestPolicy = new AuthorizationPolicyBuilder()
//             .RequireAuthenticatedUser()
//             .RequireClaim("scope", "sji3_api")
//             .Build();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
});

builder.Services.AddScoped<IAuthorizationHandler, RequireScopeHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SJI3ApiPolicy", policyUser =>
    {
        policyUser.Requirements.Add(new RequireScope());
    });
});

builder.Services
    .AddOpenIddict()
    .AddValidation(options =>
    {
        // Note: the validation handler uses OpenID Connect discovery
        // to retrieve the address of the introspection endpoint.
        options.SetIssuer("https://localhost:44395/");
        options.AddAudiences("sji3_api_resource_server");

        // Configure the validation handler to use introspection and register the client
        // credentials used when communicating with the remote introspection endpoint.
        options.UseIntrospection()
                .SetClientId("sji3_api_resource_server")
                .SetClientSecret("846B62D0-DEF9-4215-A99D-86E6B8DAB342");

        // Register the System.Net.Http integration.
        options.UseSystemNetHttp();

        // Register the ASP.NET Core host.
        options.UseAspNetCore();
    });

builder.Services.AddSwaggerGen(c =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] { }}
    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SJI3 Api",
        Version = "v1",
        Description = "SJI3 Api",
        Contact = new OpenApiContact
        {
            Name = "SJI3"
        }
    });
});

builder.Services
    .AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opt.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        opt.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
    });

builder.Services.AddValidatorsFromAssembly(AppDomain.CurrentDomain.Load("SJI3.Core"));
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

var config = TypeAdapterConfig.GlobalSettings;
config.Scan(AppDomain.CurrentDomain.Load("SJI3.Core"), AppDomain.CurrentDomain.Load("SJI3.Infrastructure"));
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumers(AppDomain.CurrentDomain.Load("SJI3.Core"));
});

builder.Services.AddMassTransit<IMemoryBus>(cfg =>
{
    cfg.SetKebabCaseEndpointNameFormatter();

    cfg.AddConsumer<TaskStatusChangedConsumer>(typeof(TaskStatusChangedConsumerDefinition));
    cfg.AddConsumer<TaskUnitAddedConsumer>(typeof(TaskUnitAddedConsumerDefinition));
    
    cfg.UsingInMemory((context, configurator) =>
    {
        configurator.ConfigureEndpoints(context);
    });
});

builder.Services.AddMassTransit(cfg =>
{
    cfg.SetKebabCaseEndpointNameFormatter();

    cfg.AddConsumer<ApplicationUserCreatedConsumer>(typeof(ApplicationUserCreatedConsumerDefinition));
    
    cfg.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        configurator.ConfigureEndpoints(context);
    });
});

builder.Services.Scan(scan => scan.FromAssemblies(AppDomain.CurrentDomain.Load("SJI3.Core"))
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Validator")))
    .AsImplementedInterfaces()
    .WithTransientLifetime());

builder.Services.Scan(scan => scan.FromAssemblies(AppDomain.CurrentDomain.Load("SJI3.Infrastructure"))
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
    .AsImplementedInterfaces()
    .WithTransientLifetime());

builder.Services
    .AddDbContext<AppDbContext>(opts =>
    {
        opts
            .UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                optionsBuilder =>
                {
                    optionsBuilder.UseNodaTime();
                })
            .EnableSensitiveDataLogging();
    })
    .AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

builder.Services.AddTransient(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
builder.Services.AddTransient<ITypeHelperService, TypeHelperService>();
builder.Services.AddTransient<ITaskProcessingService, TaskProcessingService>();
builder.Services.AddTransient<IDomainEventPublishingService, DomainEventPublishingService>();

builder.Services.AddHostedService<SeedBackgroundService>();
builder.Services.AddHostedService<TaskProcessingBackgroundService>();
builder.Services.AddHostedService<DomainEventsPublishingBackgroundService>();

builder.Services.AddRefitClient<IDemoClientApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(
        builder.Configuration.GetSection(nameof(IntegrationApisConfiguration))
            .Get<IntegrationApisConfiguration>()
            .DemoClientBaseAddress))
    .AddPolicyHandler(PollyPolicies.RetryWithJitter(new PolicyOption(TimeSpan.FromSeconds(1), 3)));

builder.Services.AddWcfClient<IService>(c => new WcfServiceClientOptions
{
    ServiceUrl = builder.Configuration.GetSection(nameof(IntegrationApisConfiguration))
        .Get<IntegrationApisConfiguration>()
        .DemoWcfClientServiceUrl
});

builder.Services.AddSingleton<ITaskQueue<Guid>>(_ =>
{
    if (!int.TryParse(builder.Configuration["QueueCapacity"], out var queueCapacity))
        queueCapacity = 100;
    return new BackgroundTaskQueue(queueCapacity);
});

builder.Services.AddSingleton<ITaskQueue<IDomainEvent>>(_ =>
{
    if (!int.TryParse(builder.Configuration["QueueCapacity"], out var queueCapacity))
        queueCapacity = 100;
    return new BackgroundDomainEventQueue(queueCapacity);
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(ui => ui.SwaggerEndpoint("/swagger/v1/swagger.json", "SJI3 Api"));

app.UseMiddleware<ExceptionHandler>();

app.UseCors(allowAllOrigins);

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<NotificationsHub>("/hub/notificationhub");
});

try
{
    Log.Information("Starting up");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}