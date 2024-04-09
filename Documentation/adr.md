# Architectural Decision Records
- CA - Clean Architecture
- DDD - Domain Driven Design
- CQRS - Command and Query Responsability Segregation
---
## CA
**Shared Kernel**
- Abstractions
- Utilities
- Configuration

**Core**
- Domain - Value Objects, Entities, Aggregates
- Aplication - Process Logic, Transactions, CQRS Commands / Queries

**Infrstructure** 
- Write Model using Entity Framework
- Read Model using Dapper

**Presentation** 
- Discord Application
- Api Controllers

**Host**
- ASP.NET Host with Blazor
---
## Strategic dependencies in the **Shared Kernel**
- ~~ASPNET.Core Identity, Authentication and Authorization packages~~
- ~~https://blazorise.com/docs~~
- https://github.com/vkhorikov/CSharpFunctionalExtensions
- https://github.com/Cysharp/Ulid