using VirginMedia.Sales.Web.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace VirginMedia.Sales.Web.HttpRepository
{
    public class ProductSalesHttpRepository : IProductSalesHttpRepository
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public ProductSalesHttpRepository(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<Response<IEnumerable<ProductSalesModel>>> GetProductSales(ProductSalesParameters productSalesParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productSalesParameters.PageNumber.ToString()
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString("productsales", queryStringParam));
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException();
            }
            var responseData = response.Content.ReadFromJsonAsync<Response<IEnumerable<ProductSalesModel>>>();
            
            return responseData.Result;
        }
    }
}
