using System.Collections.Generic;
using System.Linq.Expressions;
using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Shouldly;
using VirginMedia.Sales.Api;
using VirginMedia.Sales.Application.Handlers;
using VirginMedia.Sales.Application.Mapping;
using VirginMedia.Sales.Application.Queries;
using VirginMedia.Sales.Domain.Entities;
using VirginMedia.Sales.Domain.Repositories;
using VirginMedia.Sales.Infrastructure.Repositories;
using VirginMedia.Sales.Web.Models;
using Xunit;

namespace GOTG.Services.POSArticles.UnitTests.Features.ListTests;

public class GetProductSalesListHandlerShould
{
    private GetProductSalesListHandler _handler;
    private IQueryable<ProductSalesEntity> _productSales;
    private readonly Fixture _fixture;
    private readonly Mock<IProductSalesRepository> _productSalesRepository;
    private readonly IMapper _mapper;
    public GetProductSalesListHandlerShould()
    {
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(MappingProfile));
        }));
        _fixture = new Fixture();
        _productSalesRepository = new Mock<IProductSalesRepository>();
        _productSales = _fixture.CreateMany<ProductSalesEntity>().AsQueryable();
        _productSalesRepository
            .Setup(m => m.GetAll())
            .Returns(_productSales);
        _handler = new GetProductSalesListHandler(_productSalesRepository.Object, _mapper);
    }

    [Fact]
    public async Task GetAllProductSales()
    {
        // Given
        var query = new GetProductSalesQuery(0, 10);
        var firstProductSale = _productSales.FirstOrDefault();
        // When
        var result = await _handler.Handle(query, CancellationToken.None);

        // Then
        result.ShouldNotBeNull();
        result.Data.ShouldNotBeNull();
        var firstResult = result.Data.FirstOrDefault();

        firstResult.ShouldNotBeNull();
        firstResult.Product.ShouldBe(firstProductSale?.Product);
    }
}