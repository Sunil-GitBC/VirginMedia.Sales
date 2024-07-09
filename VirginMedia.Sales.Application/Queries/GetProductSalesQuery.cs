using MediatR;
using VirginMedia.Sales.Application.Models;
using VirginMedia.Sales.Web.Models;

namespace VirginMedia.Sales.Application.Queries
{
    public class GetProductSalesQuery : IRequest<Response<IEnumerable<ProductSalesModel>>>
    {
        public GetProductSalesQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
