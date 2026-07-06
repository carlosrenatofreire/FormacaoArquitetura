using Architecture.Business.Interfaces.Internals.Entities;
using Architecture.Business.Interfaces.Shareds;
using Architecture.Business.Models.Internals.Entities;
using Architecture.Business.Services.Shareds;

namespace Architecture.Business.Services.Internals.Entities
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository,
                              INotifier notifier) 
                              : base(notifier)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {
            // TODO: Implementar a validacao de negocio (Fluent Validation)

            // TODO: Implementar a validacao no banco de Dados 

            await _productRepository.Add(product);

        }

        public async Task Update(Product product)
        {
            // TODO: Implementar a validacao de negocio (Fluent Validation)

            await _productRepository.Update(product);
        }

        public async Task Remove(Guid id)
        {
            // TODO: Implementar a validacao de negocio (Fluent Validation)

            await _productRepository.Remove(id);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }

    }
}
