using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using SJI3.API.Common;
using SJI3.Infrastructure.Data;
using SJI3.Infrastructure.JobScheduler;
using SJI3.Infrastructure.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
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
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
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
config.Scan(AppDomain.CurrentDomain.Load("SJI3.Core"));
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumers(AppDomain.CurrentDomain.Load("SJI3.Core"));
});

builder.Services.AddMassTransit(cfg =>
{
    cfg.SetKebabCaseEndpointNameFormatter();
    cfg.AddConsumers(AppDomain.CurrentDomain.Load("SJI3.Infrastructure"));
    cfg.UsingInMemory((context, configurator) =>
    {
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

builder.Services.AddSingleton<IJobFactory, JobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddTransient<ITaskProcessingService, TaskProcessingService>();

builder.Services.AddHostedService<SeedBackgroundService>();
builder.Services.AddHostedService<TaskProcessingBackgroundService>();

builder.Services.AddRefitClient<IDemoClientApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(
        builder.Configuration.GetSection(nameof(IntegrationApisConfiguration))
            .Get<IntegrationApisConfiguration>()
            .DemoClientBaseAddress));

builder.Services.AddSingleton<ITaskQueue<Guid>>(_ =>
{
    if (!int.TryParse(builder.Configuration["QueueCapacity"], out var queueCapacity))
        queueCapacity = 100;
    return new BackgroundTaskQueue(queueCapacity);
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandler>();

app.UseCors(allowAllOrigins);

app.UseRouting();
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