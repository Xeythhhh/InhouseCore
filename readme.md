# InhouseCore
InhouseCore is a comprehensive and customizable **ranking system** for gaming communities that demand accuracy, reliability, and customization. It's ideal for communities in niche games like Battlerite that lack official support from developers, but any gaming community that needs a ranking system can benefit from InhouseCore. 

---
## Contents
[Features](#Features)

[Architecture](#Architecture)
- [Build](#Build)
- [Host](#Host)
- [Presentation](#Presentation)
- [Domain](#Domain)
- [Application](#Application)
- [Infrastructure](#Infrastructure)
- [Shared Kernel](#Shared-Kernel)
- [Tests](#Tests)

[Target Audience](#Target-Audience)

[Installation](#Installation)

[Usage](#Usage)

[Contributing](#Contributing)

[Credits](#Credits)

[Contributors](#Contributors)

[Libraries](#Libraries)

[License](#License)

---
## Features
InhouseCore includes but is not limited to the following features:
- MMR
- Configurable Queue Systems
- Matchmaking
- Configurable Draft
- User profiles
- Stats for Users, Maps, Characters, Regions
- Tournaments / Events / Custom Matches
- Seasons
> A full list of features is included in the documentation
---
## Architecture
[Architectural Design Records](Documentation/adr.md)

InhouseCore tries to follow [Clean Code](https://gist.github.com/wojteklu/73c6914cc446146b8b533c0988cf8d29) and [Domain Driven Design](https://en.wikipedia.org/wiki/Domain-driven_design) principles.

### Build
- [Nuke](https://nuke.build) Build System 
### Host
- [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet) API Host / [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor) WASM with [MudBlazor](https://mudblazor.com)
### Presentation
- [DSharpPlus](https://dsharpplus.github.io/DSharpPlus/) [Discord](https://discord.com) Application
### Application
- Process Logic
- Transactions
- Commands
- Queries
- Behaviors
### Domain
- Rich Domain Model
- Entities
- Value Objects
- Aggregates
### Infrastructure
- Read Model with [Dapper](https://github.com/DapperLib/Dapper)
- Write Model with [Entity Framework](https://learn.microsoft.com/en-us/ef/)
### Shared Kernel
- Abstractions
- Utilities
- Constants
- Contracts
- Extensions
- Result Pattern Implementation
### Tests
- [xUnit](https://xunit.net) Tests
- [NetArchTest](https://www.ben-morris.com/writing-archunit-style-tests-for-net-and-c-for-self-testing-architectures/) Tests
---
## Target Audience
InhouseCore is designed for gaming communities that require a comprehensive and customizable **ranking system**. It's ideal for communities in niche games like Battlerite that lack official support from developers, but any gaming community that needs a ranking system can benefit from InhouseCore. The system is tailored to the needs of high-level players looking to compete at the highest level, providing accurate and reliable matchmaking and MMR tracking.

---
## Installation
[Instructions on how to install and set up InhouseCore will go here].

---
## Usage
[Instructions on how to use InhouseCore, including command line examples, API documentation, and sample code, will go here]

---
## Contributing
Contributions are welcome! Please read the [contribution](Documentation/contributing.md) and [design](Documentation/guidelines.md) guidelines before submitting a pull request.

---
## Credits
### Contributors
- [Xeyth](https://github.com/Xeythhhh) - Author
- [Mingo](https://github.com/arialucia) - Author
- [Battlerite Community League](https://discord.gg/bcl) - Feedback
### Libraries
- [AspNetCore](https://github.com/dotnet/aspnetcore)
- [Blazor](https://blazor.net/) - Framework for client-side web UI with .NET
- [MudBlazor](https://mudblazor.com) - Blazor Component Library
- [EFCore](https://github.com/dotnet/efcore) - Object-database mapper for .NET
- [Dapper](https://github.com/DapperLib/Dapper)
- [DSharpPlus](https://github.com/DSharpPlus/DSharpPlus) - .NET wrapper for the Discord API
- [IdGen](https://github.com/RobThree/IdGen) - Twitter Snowflake-alike ID generator for .NET
- [NetArchTest](https://www.ben-morris.com/writing-archunit-style-tests-for-net-and-c-for-self-testing-architectures/) - Architecture Tests
- [FluentAssertions](https://fluentassertions.com) - Prettier Assertions

---
## License
InhouseCore is licensed under the [MIT License](LICENSE).