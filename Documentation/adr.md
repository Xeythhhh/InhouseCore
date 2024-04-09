# Architectural Decision Records
- CA - Clean Architecture
- DDD - Domain Driven Design
- CQRS - Command and Query Responsability Segregation
---

**Host**
- ASP.NET Host with Blazor

**Build**
- Nuke Build

**Shared Kernel**
- Abstractions
- Utilities
- Configuration

## **Core**
- Domain 
	- Value Objects
	- Entities
	- Aggregates
- Aplication 
	- Process Logic
	- Transactions
	- Commands and Queries

## **External**
- Infrastructure
	- Write Model using Entity Framework
	- Read Model using Dapper
- Presentation 
	- Discord Application
	- Api Controllers
	- Blazor Components

---
## Strategic dependencies in the **Shared Kernel**
- https://github.com/vkhorikov/CSharpFunctionalExtensions
- https://github.com/Cysharp/Ulid