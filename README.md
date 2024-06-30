<img width=128 height=128 src='https://github.com/mStylias/Harmony/assets/57811193/63febb60-ee4f-47a5-9f5e-acc154f700ef' alt='harmony-logo'>

# What is Harmony?
Harmony is a set of code structuring tools that aim to simplify the creation of well structured, systematic and easily testable .NET code bases. It provides the foundation to implement modern programming paradigms, like railway oriented programming, and common design patterns like mediator and cqrs in a flexible way. Harmony has some opinionated elements, but overall it aims to be as flexible as possible, so it can be used in various different systems and architectures.

# NuGet Packages
Harmony includes three packages, all requiring a minimum of .NET 6 and fully supported up to .NET 8.

Harmony Package                                                           | NuGet                                                                                                                                                         |
------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------- |
[Harmony.Results](https://www.nuget.org/packages/Harmony.Results)         | [<img src='https://img.shields.io/nuget/v/Harmony.Results' alt='harmony-results-nuget-version'>](https://www.nuget.org/packages/Harmony.Results)              |
[Harmony.Cqrs](https://www.nuget.org/packages/Harmony.Cqrs)               | [<img src='https://img.shields.io/nuget/v/Harmony.Cqrs' alt='harmony-cqrs-nuget-version'>](https://www.nuget.org/packages/Harmony.Cqrs)                       |
[Harmony.MinimalApis](https://www.nuget.org/packages/Harmony.MinimalApis) | [<img src='https://img.shields.io/nuget/v/Harmony.MinimalApis' alt='harmony-minimal-apis-nuget-version'>](https://www.nuget.org/packages/Harmony.MinimalApis) |

Harmony.Results and Harmony.Cqrs are completely independent of each other, while Harmony.MinimalApis has a dependency on Harmony.Results.

# Documentation
A complete documentation is available at the [wiki](https://github.com/mStylias/Harmony/wiki) of this repository. Also a sample todo api that uses Harmony is included in the project files as an example.
