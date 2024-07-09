using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using VirginMedia.Sales.Api;
using VirginMedia.Sales.Application.Models;
using VirginMedia.Sales.Application.Queries;
using VirginMedia.Sales.Web.Models;
using Xunit;

namespace GOTG.Services.POSArticles.UnitTests.Controllers;

public class ProductSalesControllerShould
{
    private readonly ProductSalesController _controller;
    private readonly Mock<IMediator> _mockMediator;
    private readonly Mock<ILogger<ProductSalesController>> _mockLogger;
    private readonly Fixture _fixture;

    public ProductSalesControllerShould()
    {
        _fixture = new Fixture();
        _mockLogger = new Mock<ILogger<ProductSalesController>>();
        _mockMediator = new Mock<IMediator>();
        _controller = new ProductSalesController(_mockLogger.Object, _mockMediator.Object);
    }
    
    [Fact]
    public async Task ReturnExpectedResult()
    {
        // Given
        var query = new ProductSalesParameters()
        {
            PageNumber = 0,
            PageSize = 10
        };
        var response = _fixture.Create<Response<IEnumerable<ProductSalesModel>>>();
        response.Success = true;

        _mockMediator
           .Setup(m => m.Send(It.IsAny<GetProductSalesQuery>(), CancellationToken.None))
           .ReturnsAsync(response);

        // When
        var result = await _controller.Get(query);
        
        // Then
        result.ShouldNotBeNull();
        result.ShouldBeOfType<OkObjectResult>();

        var okResult = result as OkObjectResult;

        okResult.ShouldNotBeNull();
        okResult.StatusCode.ShouldBe(StatusCodes.Status200OK);

        var value = okResult.Value as Response<IEnumerable<ProductSalesModel>>;

        value.ShouldNotBeNull();
        value.Success.ShouldBeTrue();
    }
}