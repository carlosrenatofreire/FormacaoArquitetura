using Architecture.API.ViewModels;
using Architecture.Business.Models.Internals.Entities;
using Architecture.Business.Models.Internals.Parameters;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Architecture.API.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //CreateMap<Supplier, SupplierViewModel>().ReverseMap();
            //CreateMap<Address, AddressViewModel>().ReverseMap();
            //CreateMap<ProductViewModel, Product>();

            //CreateMap<Product, ProductViewModel>()
            //    .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name));

            // 1. Fornecedor (Entities) -> ViewModel
            CreateMap<Architecture.Business.Models.Internals.Entities.Supplier, SupplierViewModel>()
                .ReverseMap();

            // 2. Endereço (Parameters) -> ViewModel
            CreateMap<Architecture.Business.Models.Internals.Parameters.Address, AddressViewModel>()
                .ReverseMap();

            // 3. Produto (Entities) -> ViewModel
            CreateMap<Architecture.Business.Models.Internals.Entities.Product, ProductViewModel>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.Name : null))
                .ReverseMap();
        }
    }
}
