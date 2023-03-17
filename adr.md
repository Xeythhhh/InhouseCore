# Architectural Decision Records

- CA - Clean Architecture
- DDD - Domain Driven Design
- CQRS - Command and Query Responsability Segregation

---

CA

- **Shared** - can be reused accross projects
	- Abstractions
	- Utilities
- **Core**
	- Domain - Value Objects, Entities, Aggregates
	- Aplication - Process Logic, Transactions, CQRS Commands / Queries
- **Infrstructure** 
	- EF Core Sqlite
- **Host**
	- Blazor WebAssembly Client App
	- Blazor Server Authentication App
	- ASP.NET Core API

---

## Strategic dependencies in the **Shared Kernel**
https://blazorise.com/docs
https://github.com/vkhorikov/CSharpFunctionalExtensions
https://docs.fluentvalidation.net/en/latest/
