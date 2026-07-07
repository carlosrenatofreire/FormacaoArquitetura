using Architecture.Business.Interfaces.Internals.Entities;
using Architecture.Business.Interfaces.Shareds;
using Architecture.Business.Models.Internals.Entities;
using Architecture.Business.Services.Shareds;
using Architecture.Business.Validations.Entities;
using Architecture.Business.Validations.Parameters;

namespace Architecture.Business.Services.Internals.Entities
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository, INotifier notifier) : base(notifier)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task Add(Supplier supplier)
        {
            // Validacao de negocio (Fluent Validation)
            if (!ExecuteValidation(new SupplierValidation(), supplier) || !ExecuteValidation(new AddressValidation(), supplier.Address)) return;

            // TODO: Implementar a validacao no banco de Dados 

            await _supplierRepository.Add(supplier);
        }

        public async Task Update(Supplier supplier)
        {
            // Validacao de negocio (Fluent Validation)
            if (!ExecuteValidation(new SupplierValidation(), supplier)) return;

            await _supplierRepository.Update(supplier);
        }
        public async Task Remove(Guid id)
        {
            await _supplierRepository.Remove(id);
        }

        public void Dispose()
        {
            _supplierRepository?.Dispose();
        }

    }
}
