using Architecture.Business.Models.Internals.Entities;

namespace Architecture.Business.Interfaces.Internals.Entities
{
    public interface ISupplierService : IDisposable
    {
        Task Add(Supplier supplier);
        Task Update(Supplier supplier);
        Task Remove(Guid id);
    }
}
