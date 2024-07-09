
using VirginMedia.Sales.Application.Models;
using VirginMedia.Sales.Domain.Entities;

namespace VirginMedia.Sales.Application.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductSalesModel, ProductSalesEntity>().ReverseMap();
        }
    }
}