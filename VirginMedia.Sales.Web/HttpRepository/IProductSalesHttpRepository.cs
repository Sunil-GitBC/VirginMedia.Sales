using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirginMedia.Sales.Web.Models;

namespace VirginMedia.Sales.Web.HttpRepository
{
    public interface IProductSalesHttpRepository
    {
        Task<Response<IEnumerable<ProductSalesModel>>> GetProductSales(ProductSalesParameters productSalesParameters);
    }
}
