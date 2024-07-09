using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using VirginMedia.Sales.Web.Models;

namespace VirginMedia.Sales.Web.Components.ProductSalesTable
{
	public partial class ProductSalesTable
    {
        [Parameter]
        public List<ProductSalesModel> ProductSales { get; set; }
    }
}
