using AutoMapper;
using MediatR;
using VirginMedia.Sales.Application.Models;
using VirginMedia.Sales.Application.Queries;
using VirginMedia.Sales.Domain.Repositories;
using VirginMedia.Sales.Web.Models;

namespace VirginMedia.Sales.Application.Handlers
{
    public class GetProductSalesListHandler : IRequestHandler<GetProductSalesQuery, Response<IEnumerable<ProductSalesModel>>>
    {
        private readonly IProductSalesRepository _productSalesRepository;
        private readonly IMapper _mapper;

        public GetProductSalesListHandler(IProductSalesRepository productSalesRepository,
             IMapper mapper)
        {
            _productSalesRepository = productSalesRepository ?? throw new ArgumentNullException(nameof(productSalesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<IEnumerable<ProductSalesModel>>> Handle(GetProductSalesQuery request, CancellationToken cancellationToken)
        {
            var productSales = _productSalesRepository.GetAll();
            int totalCount = productSales.Count();

            productSales = productSales.Skip(request.PageNumber);

            productSales = productSales.Take(request.PageSize);

            var productSalesModelList = _mapper.Map<List<ProductSalesModel>>(productSales);

            Response<IEnumerable<ProductSalesModel>> response = new Response<IEnumerable<ProductSalesModel>>()
            {
                Data = productSalesModelList,
                StatusCode = productSalesModelList == null ? System.Net.HttpStatusCode.NotFound : System.Net.HttpStatusCode.OK,
                Success = true,
                PagingData = new PagingData()
                {
                    CurrentPage = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
                }
            };
            return response;
        }
    }
}
