using Architecture.Business.Interfaces.Shareds;
using Architecture.Business.Models.Internals.Entities;

namespace Architecture.Business.Interfaces.Internals.Entities
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsBySupplierId(Guid supplierId);
        Task<IEnumerable<Product>> GetProductsAndSuppliers();
        Task<Product> GetProductAndSupplier(Guid id);
    }
}
