# Depth Chart Pro API Service

This document will guide you through the information about `DepthChartPro` solution.

### Prerequisite
- .NET 6.0
- Microsoft.EntityFrameworkCore.InMemory
- Microsoft.EntityFrameworkCore.Tools
- Newtonsoft.Json
- xunit
- Moq

### Architecture and patterns
This solution uses layered architecture. That will not only help to organize, maintainability, readability of the code but also server scalabilty on demand. Used the `Dependency Injection pattern to register the classes in the startup and used them when needed through the constructor injection. Used the repository pattern to communicate through the database and provide the data for the solution.   
