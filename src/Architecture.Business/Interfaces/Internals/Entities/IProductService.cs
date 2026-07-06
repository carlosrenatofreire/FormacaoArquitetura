using Architecture.Business.Models.Internals.Entities;

namespace Architecture.Business.Interfaces.Internals.Entities
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Remove(Guid id);
    }
}
