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

        public SupplierService(ISupplierRepository supplierRepository, INotifierService notifier) : base(notifier)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task Add(Supplier supplier)
        {
            // Validacao de negocio (Fluent Validation)
            if (!ExecuteValidation(new SupplierValidation(), supplier) || !ExecuteValidation(new AddressValidation(), supplier.Address)) return;

            // Validacao no banco de Dados 
            if (_supplierRepository.Find(f => f.Document == supplier.Document).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento infomado.");
                return;
            }

            await _supplierRepository.Add(supplier);
        }

        public async Task Update(Supplier supplier)
        {
            // Validacao de negocio (Fluent Validation)
            if (!ExecuteValidation(new SupplierValidation(), supplier)) return;

            // Validacao no banco de Dados 
            if (_supplierRepository.Find(f => f.Document == supplier.Document && f.Id != supplier.Id).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento infomado.");
            }

            await _supplierRepository.Update(supplier);
        }
        public async Task Remove(Guid id)
        {
            // Validacao no banco de Dados 
            var supplier = await _supplierRepository.GetSupplierAndProductsAndAddress(id);

            if (supplier == null)
            {
                Notify("Fornecedor não existe!");
                return;
            }

            if (supplier.Products.Any())
            {
                Notify("O fornecedor possui produtos registados!");
                return;
            }

            var address = await _supplierRepository.GetAddressBySupplierId(id);

            if (address != null)
            {
                await _supplierRepository.RemoveSupplierAddress(address);
            }

            await _supplierRepository.Remove(id);
        }

        public void Dispose()
        {
            _supplierRepository?.Dispose();
        }

    }
}
