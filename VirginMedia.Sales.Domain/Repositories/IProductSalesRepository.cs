using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirginMedia.Sales.Domain.Entities;

namespace VirginMedia.Sales.Domain.Repositories
{
    public interface IProductSalesRepository : IRepository<ProductSalesEntity>
    {
        public IQueryable<ProductSalesEntity> GetAll();
    }
}
