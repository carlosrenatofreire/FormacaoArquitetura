using Architecture.Business.Models.Internals.Entities;
using Architecture.Business.Models.Internals.Parameters;
using Architeture.Mvc.ViewModels;
using AutoMapper;

namespace Architeture.Mvc.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Supplier, SupplierViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
