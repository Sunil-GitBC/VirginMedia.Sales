using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Numerics;
using System.Text.Unicode;
using VirginMedia.Sales.Domain.Entities;
using VirginMedia.Sales.Domain.Repositories;

namespace VirginMedia.Sales.Infrastructure.Repositories
{
    public class ProductSalesCSVRepository : IProductSalesRepository
    {
        private const int SegmentColOrder = 0;
        private const int CountryColOrder = 1;
        private const int ProductColOrder = 2;
        private const int DiscountBandColOrder = 3;
        private const int UnitsSoldColOrder = 4;
        private const int ManufacturingPriceColOrder = 5;
        private const int SalePriceColOrder = 6;
        private const int DateColOrder = 7;
        private readonly ILogger<ProductSalesCSVRepository> _logger;

        public ProductSalesCSVRepository(ILogger<ProductSalesCSVRepository> logger)
        {
            _logger = logger;
        }
        public IQueryable<ProductSalesEntity> GetAll()
        {
            var productSales = File.ReadAllLines("Data/Data_UTF8.csv", System.Text.Encoding.UTF8)
                    .Skip(1)
                    .Where(row => row.Length > 0)
                    .Select(ParseRow)
                    .AsQueryable();
            return productSales;
        }

        private ProductSalesEntity ParseRow(string row)
        {
            try
            {

                var columns = row.Split(',');
                return new ProductSalesEntity()
                {
                    Segment = columns[SegmentColOrder],
                    Country = columns[CountryColOrder],
                    Product = columns[ProductColOrder],
                    DiscountBand = columns[DiscountBandColOrder],
                    UnitsSold = ParseDecimal(columns[UnitsSoldColOrder]),
                    ManufacturingPrice = ParseDecimalForCurrency(columns[ManufacturingPriceColOrder]),
                    SalePrice = ParseDecimalForCurrency(columns[SalePriceColOrder]),
                    Date = ParseDateTime(columns[DateColOrder])
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private decimal? ParseDecimalForCurrency(string value)
        {
            try
            {
                if (decimal.TryParse(value, NumberStyles.Currency, null, out decimal result))
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Unable to parse decimal value {value}");
            }
            return null;
        }

        private decimal? ParseDecimal(string value)
        {
            try
            {
                if (decimal.TryParse(value, out decimal result))
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Unable to parse decimal value {value}");
            }
            return null;
        }

        private DateTime? ParseDateTime(string value)
        {
            try
            {
                if (DateTime.TryParse(value, out DateTime result))
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Unable to parse date time value {value}");
            }
            return null;
        }
    }
}
