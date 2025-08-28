# CleanArchCqrs

Clean Architecture with CQRS combines two powerful patterns:

Clean Architecture: Organizes code in concentric circles where dependencies flow inward toward the domain

CQRS (Command Query Responsibility Segregation): Separates read operations (Queries) from write operations (Commands)

The Result: A highly maintainable, testable, and scalable application architecture

## Project Structure Setup

### Solution Architecture

Create a well-organized solution structure that reflects Clean Architecture principles:

- Create the solution directory
mkdir CleanArchCQRS.NET9
cd CleanArchCQRS.NET9
- Create the solution file
dotnet new sln -n CleanArchCQRS
- Create projects following Clean Architecture layers
dotnet new webapi -n CleanArchCQRS.API -f net9.0
dotnet new classlib -n CleanArchCQRS.Application -f net9.0
dotnet new classlib -n CleanArchCQRS.Domain -f net9.0
dotnet new classlib -n CleanArchCQRS.Infrastructure -f net9.0
dotnet new classlib -n CleanArchCQRS.SharedKernel -f net9.0
dotnet new xunit -n CleanArchCQRS.Tests.Unit -f net9.0
dotnet new xunit -n CleanArchCQRS.Tests.Integration -f net9.0
- Add projects to solution
dotnet sln add CleanArchCQRS.API CleanArchCQRS.Application CleanArchCQRS.Domain CleanArchCQRS.Infrastructure CleanArchCQRS.SharedKernel CleanArchCQRS.Tests.Unit CleanArchCQRS.Tests.Integration

### Project References Configuration

Establish proper dependency flow following Clean Architecture rules:

- API Layer references
dotnet add CleanArchCQRS.API reference CleanArchCQRS.Application
dotnet add CleanArchCQRS.API reference CleanArchCQRS.Infrastructure
- Application Layer references
dotnet add CleanArchCQRS.Application reference CleanArchCQRS.Domain
dotnet add CleanArchCQRS.Application reference CleanArchCQRS.SharedKernel
- Infrastructure Layer references
dotnet add CleanArchCQRS.Infrastructure reference CleanArchCQRS.Application
dotnet add CleanArchCQRS.Infrastructure reference CleanArchCQRS.Domain
- Domain Layer references
dotnet add CleanArchCQRS.Domain reference CleanArchCQRS.SharedKernel
- Test project references
dotnet add CleanArchCQRS.Tests.Unit reference CleanArchCQRS.Application
dotnet add CleanArchCQRS.Tests.Unit reference CleanArchCQRS.Domain
dotnet add CleanArchCQRS.Tests.Integration reference CleanArchCQRS.API

### Enhanced Folder Structure

Organize each project with clear folder hierarchies:

CleanArchCQRS.NET9/
├── CleanArchCQRS.API/                    # Presentation Layer
│   ├── Controllers/
│   ├── Endpoints/                        # Minimal API endpoints
│   ├── Middleware/
│   ├── Filters/
│   ├── Extensions/
│   └── Program.cs
├── CleanArchCQRS.Application/            # Application Layer
│   ├── Common/
│   │   ├── Behaviors/                    # MediatR pipeline behaviors
│   │   ├── Exceptions/
│   │   ├── Interfaces/
│   │   ├── Mappings/
│   │   └── Models/
│   ├── Features/                         # Feature-based organization
│   │   ├── Users/
│   │   │   ├── Commands/
│   │   │   │   ├── CreateUser/
│   │   │   │   ├── UpdateUser/
│   │   │   │   └── DeleteUser/
│   │   │   └── Queries/
│   │   │       ├── GetUser/
│   │   │       └── GetUsers/
│   │   └── Products/
│   │       ├── Commands/
│   │       └── Queries/
│   └── DependencyInjection.cs
├── CleanArchCQRS.Domain/                 # Domain Layer
│   ├── Entities/
│   ├── ValueObjects/
│   ├── Enums/
│   ├── Events/                          # Domain events
│   ├── Exceptions/
│   ├── Repositories/                    # Repository interfaces
│   └── Services/                        # Domain services
├── CleanArchCQRS.Infrastructure/         # Infrastructure Layer
│   ├── Data/
│   │   ├── Contexts/
│   │   ├── Configurations/
│   │   └── Repositories/
│   ├── Services/
│   │   ├── Email/
│   │   ├── Storage/
│   │   └── External/
│   └── DependencyInjection.cs
├── CleanArchCQRS.SharedKernel/           # Shared abstractions
│   ├── Common/
│   │   ├── BaseEntity.cs
│   │   ├── IDomainEvent.cs
│   │   └── Result.cs
│   └── Extensions/
├── CleanArchCQRS.Tests.Unit/
└── CleanArchCQRS.Tests.Integration/

## NuGet Package Configuration

### Package Installation by Layer

Install the appropriate packages for each layer:

- API Layer packages
dotnet add CleanArchCQRS.API package Microsoft.EntityFrameworkCore.Design
dotnet add CleanArchCQRS.API package Serilog.AspNetCore
dotnet add CleanArchCQRS.API package Swashbuckle.AspNetCore
- Application Layer packages
dotnet add CleanArchCQRS.Application package MediatR
dotnet add CleanArchCQRS.Application package FluentValidation
dotnet add CleanArchCQRS.Application package AutoMapper
dotnet add CleanArchCQRS.Application package Microsoft.Extensions.DependencyInjection.Abstractions
- Infrastructure Layer packages
dotnet add CleanArchCQRS.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
dotnet add CleanArchCQRS.Infrastructure package Microsoft.EntityFrameworkCore.Tools
dotnet add CleanArchCQRS.Infrastructure package Microsoft.Extensions.Configuration
dotnet add CleanArchCQRS.Infrastructure package Microsoft.Extensions.Logging
- Test packages
dotnet add CleanArchCQRS.Tests.Unit package FluentAssertions
dotnet add CleanArchCQRS.Tests.Unit package Moq
dotnet add CleanArchCQRS.Tests.Integration package Microsoft.AspNetCore.Mvc.Testing
dotnet add CleanArchCQRS.Tests.Integration package Microsoft.EntityFrameworkCore.InMemory

### Directory.Build.props Configuration

Create a centralized package management system:

<!-- Directory.Build.props -->
<Project>
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <ImplicitUsings>enable</ImplicitUsings>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
  </PropertyGroup>
<ItemGroup>
    <PackageReference Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="9.0.0" />
  </ItemGroup>
  <!-- MediatR packages -->
  <ItemGroup Condition="'$(MSBuildProjectName)' == 'CleanArchCQRS.Application' OR '$(MSBuildProjectName)' == 'CleanArchCQRS.API'">
    <PackageReference Include="MediatR" Version="12.4.1" />
  </ItemGroup>
  <!-- Entity Framework packages -->
  <ItemGroup Condition="'$(MSBuildProjectName)' == 'CleanArchCQRS.Infrastructure' OR '$(MSBuildProjectName)' == 'CleanArchCQRS.API'">
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.0" />
  </ItemGroup>
  <!-- Test packages -->
  <ItemGroup Condition="$(MSBuildProjectName.EndsWith('.Tests'))">
    <PackageReference Include="xunit" Version="2.9.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.8.2" />
    <PackageReference Include="FluentAssertions" Version="6.12.1" />
    <PackageReference Include="Moq" Version="4.20.72" />
  </ItemGroup>
</Project>

## Domain Layer Implementation

### SharedKernel Foundation

Create shared abstractions that will be used across layers:

- CleanArchCQRS.SharedKernel/Common/BaseEntity.cs
- CleanArchCQRS.SharedKernel/Common/IDomainEvent.cs
- CleanArchCQRS.SharedKernel/Common/Result.cs

### Domain Entities and Value Objects

Implement rich domain models with proper encapsulation:

- CleanArchCQRS.Domain/ValueObjects/UserId.cs
- CleanArchCQRS.Domain/ValueObjects/Email.cs
- CleanArchCQRS.Domain/Entities/User.cs
- CleanArchCQRS.Domain/Events/UserDomainEvents.cs

### Repository Interfaces

Define repository contracts in the domain layer:

- CleanArchCQRS.Domain/Repositories/IUserRepository.cs
- CleanArchCQRS.Domain/Repositories/IUnitOfWork.cs

## Application Layer with CQRS

### MediatR Pipeline Behaviors

Implement cross-cutting concerns through pipeline behaviors:

- CleanArchCQRS.Application/Common/Behaviors/ValidationBehavior.cs
- CleanArchCQRS.Application/Common/Behaviors/LoggingBehavior.cs
- CleanArchCQRS.Application/Common/Behaviors/PerformanceBehavior.cs