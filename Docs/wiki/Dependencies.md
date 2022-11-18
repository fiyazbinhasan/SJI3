# Dependencies

## Core

- CSharpFunctionalExtensions
- FluentValidation
- Mapster
- NodaTime
- MassTransit.Analyzers
- MassTransit

> Dependency On: None

## Infrastrucure:

- Microsoft.EntityFrameworkCore
- Npgsql.EntityFrameworkCore.PostgreSQL
- Npgsql.EntityFrameworkCore.PostgreSQL.NodaTime
- Npgsql
- Npgsql.NodaTime
- Quartz.Extensions.Hosting
- System.Linq.Dynamic.Core
- Microsoft.AspNetCore.SignalR.StackExchangeRedis
- Quartz
- Refit

> Dependency On: Core

## API

- Mapster.DependencyInjection
- Microsoft.EntityFrameworkCore.Tools
- FluentValidation.AspNetCore
- Microsoft.Extensions.Logging.Debug
- Microsoft.VisualStudio.Azure.Containers.Tools.Targets
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Refit.HttpClientFactory
- Scrutor
- Serilog.AspNetCore
- NodaTime.Serialization.SystemTextJson
- Swashbuckle.AspNetCore
- MassTransit.Analyzers
- MassTransit.AspNetCore

> Dependency On: Core, Infrastructure

## Worker

- Microsoft.Extensions.Hosting
- Microsoft.VisualStudio.Azure.Containers.Tools.Targets

> Dependency On: Core, Infrastructure

## Identity
