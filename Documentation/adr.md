# Architectural Decision Records
- CA - Clean Architecture
- DDD - Domain Driven Design
- CQRS - Command and Query Responsibility Segregation
---

**Host**
- ASP.NET API Host with Blazor

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

- Application 
	- Process Logic
	- Transactions
	- Commands
	- Queries

## **External**
- Infrastructure
	- Write Model using Entity Framework
	- Read Model using Dapper

- Presentation 
	- Discord Application

---
## Strategic dependencies in the **Shared Kernel**
- FluentAssertions
- FluentValidation
- Microsoft.Extensions.Configuration.Abstractions