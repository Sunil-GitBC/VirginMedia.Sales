Below is a brief summary of the test.

The solution includes the following projects
  •	VirginMedia.Sales.Api
  •	VirginMedia.Sales.Application
  •	VirginMedia.Sales.Domain
  •	VirginMedia.Sales.Infrastructure
  •	VirginMedia.Sales.UnitTests
  •	VirginMedia.Sales.Web which is a Blazor Project
  •	VirginMedia.Sales.Web.Models

I have designed the application around Clean Architecture principles to achieve DDD and CQRS design using MediatR, The application itself is divided into the following layers
  Domain
  Application
  Infrastructure

Dependency injection principle has been widely used wherever applicable to make the components loosely coupled and also easily testable.

The unit test projects uses Moq and xUnit frameworks

I would have done some further refactoring to make the code more resilient and also increase code coverage by adding more tests and added more features to the Grid to add sorting and improve CSS.
