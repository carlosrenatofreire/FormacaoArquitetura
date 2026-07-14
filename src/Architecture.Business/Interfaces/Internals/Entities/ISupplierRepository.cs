using Architecture.Business.Interfaces.Shareds;
using Architecture.Business.Models.Internals.Entities;
using Architecture.Business.Models.Internals.Parameters;

namespace Architecture.Business.Interfaces.Internals.Entities
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> GetSupplierAndAddress(Guid id);
        Task<Supplier> GetSupplierAndProductsAndAddress(Guid id);
        Task<IEnumerable<Supplier>> GetAllSuppliersAndProductsAndAddresses();

        Task<Address> GetAddressBySupplierId(Guid supplierId);
        Task RemoveSupplierAddress(Address address);
    }
}
