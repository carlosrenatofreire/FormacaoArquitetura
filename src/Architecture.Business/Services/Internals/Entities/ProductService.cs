using Architecture.Business.Interfaces.Internals.Entities;
using Architecture.Business.Interfaces.Shareds;
using Architecture.Business.Models.Internals.Entities;
using Architecture.Business.Services.Shareds;
using Architecture.Business.Validations.Entities;

namespace Architecture.Business.Services.Internals.Entities
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, INotifierService notifier, IUnitOfWork unitOfWork)
            : base(notifier, unitOfWork)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {
            // Validacao de negocio (Fluent Validation)
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            // TODO: Implementar a validacao no banco de Dados

            await _productRepository.Add(product);
            await Commit();
        }

        public async Task Update(Product product)
        {
            // Validacao de negocio (Fluent Validation)
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Update(product);
            await Commit();
        }

        public async Task Remove(Guid id)
        {
            await _productRepository.Remove(id);
            await Commit();
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }

    }
}
