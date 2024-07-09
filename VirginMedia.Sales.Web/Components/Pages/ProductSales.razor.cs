
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirginMedia.Sales.Web.HttpRepository;
using VirginMedia.Sales.Web.Models;

namespace VirginMedia.Sales.Web.Components.Pages
{
    public partial class ProductSales
    {
        public List<ProductSalesModel> ProductSalesList { get; set; } = new List<ProductSalesModel>();
        public PagingData PagingData { get; set; } = new PagingData();

        private ProductSalesParameters _productParameters = new ProductSalesParameters();

        [Inject]
        public IProductSalesHttpRepository ProductSalesRepo { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetProductSales();
        }

        private async Task SelectedPage(int page)
        {
            _productParameters.PageNumber = page;
            await GetProductSales();
        }

        private async Task GetProductSales()
        {
            var pagingResponse = await ProductSalesRepo.GetProductSales(_productParameters);
            ProductSalesList = pagingResponse.Data.ToList();
            PagingData = pagingResponse.PagingData;
        }

    }
}
