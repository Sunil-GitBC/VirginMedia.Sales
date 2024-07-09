using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirginMedia.Sales.Domain.Entities
{
    public class ProductSalesEntity : BaseEntity
    {
        public string Segment { get; set; }
        public string Country { get; set; }
        public string Product { get; set; }
        public string DiscountBand { get; set; }
        public decimal? UnitsSold { get; set; }
        public decimal? ManufacturingPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public DateTime? Date { get; set; }
    }
}
