# Architectural Decision Records
- CA - Clean Architecture
- DDD - Domain Driven Design
- CQRS - Command and Query Responsability Segregation
---
## CA
- **Shared Kernel** - can be reused accross projects
	- Abstractions
	- Utilities
	- Configuration
- **Core**
	- Domain - Value Objects, Entities, Aggregates
	- Aplication - Process Logic, Transactions, CQRS Commands / Queries
	- Discord Engine - Process Discord requests
- **Infrstructure** 
	- EF Core Sqlite
- **Host**
	- Blazor WebAssembly Client App
	- Blazor Server Authentication App
	- ASP.NET Core API
---
## Strategic dependencies in the **Shared Kernel**
- ASPNET.Core Identity, Authentication and Authorization packages
- https://blazorise.com/docs
- https://github.com/vkhorikov/CSharpFunctionalExtensions
- https://docs.fluentvalidation.net/en/latest/
- https://github.com/Cysharp/Ulid