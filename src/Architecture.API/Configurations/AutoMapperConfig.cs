using Architecture.API.ViewModels;
using Architecture.Business.Models.Internals.Entities;
using Architecture.Business.Models.Internals.Parameters;
using AutoMapper;

namespace Architecture.API.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // 1. Fornecedor (Entities) -> ViewModel
            CreateMap<Architecture.Business.Models.Internals.Entities.Supplier, SupplierViewModel>()
                .ReverseMap();

            // 2. Endereço (Parameters) -> ViewModel
            CreateMap<Architecture.Business.Models.Internals.Parameters.Address, AddressViewModel>()
                .ReverseMap();

            // 3. Produto (Entities) -> ViewModel
            CreateMap<Architecture.Business.Models.Internals.Entities.Product, ProductViewModel>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.Name : null))
                .ReverseMap()
                // Sem isto, o ReverseMap gera un-flattening automático de SupplierName e
                // reconstrói um Supplier "fantasma" (só com Name preenchido) em Product.Supplier,
                // que o EF tenta inserir como uma nova entidade e viola a FK.
                .ForPath(dest => dest.Supplier!.Name, opt => opt.Ignore());
        }
    }
}
