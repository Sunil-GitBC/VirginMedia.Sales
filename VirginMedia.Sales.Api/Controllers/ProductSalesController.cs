using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VirginMedia.Sales.Application.Models;
using VirginMedia.Sales.Application.Queries;
using VirginMedia.Sales.Web.Models;

namespace VirginMedia.Sales.Api;

[Route("api/productsales")]
[ApiController]
public class ProductSalesController : ControllerBase
{

    private readonly ILogger<ProductSalesController> _logger;
    private readonly IMediator _mediator;
    public ProductSalesController(ILogger<ProductSalesController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response<IEnumerable<ProductSalesModel>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get([FromQuery] ProductSalesParameters productSalesParameters)
    {
        try
        {

            var response = await _mediator.Send(new GetProductSalesQuery(productSalesParameters.PageNumber, productSalesParameters.PageSize));

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response);
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.BadRequest:
                    return BadRequest(response.Message);
                default:
                    return Ok(response);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving product sales list: {ex.ToString()}");
            return StatusCode(500, "Internal server error");
        }
    }
}