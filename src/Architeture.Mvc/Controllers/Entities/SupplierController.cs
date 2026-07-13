using Architecture.Business.Interfaces.Internals.Entities;
using Architecture.Business.Interfaces.Shareds;
using Architecture.Business.Models.Internals.Entities;
using Architeture.Mvc.Controllers.Shareds;
using Architeture.Mvc.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Architeture.Mvc.Controllers.Entities
{
    public class SupplierController : BaseController
    {
        private readonly ISupplierService _supplierService;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        public SupplierController(ISupplierService supplierService,
                                  ISupplierRepository supplierRepository,
                                  IMapper mapper,
                                  INotifierService notifier) : base(notifier) 
        {
            _supplierService = supplierService;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        [Route("supplier-list")]
        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierRepository.GetAll();
            var mappedSuppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
            return View(mappedSuppliers);
        }

        [AllowAnonymous]
        [Route("supplier-details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var supplierViewModel = await GetAddressSupplier(id);

            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

        [HttpGet]
        [Route("supplier-new")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("supplier-new")]
        [HttpPost]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid) return View(supplierViewModel);

            var supplier = _mapper.Map<Supplier>(supplierViewModel);
            await _supplierService.Add(supplier);

            if (!ValidOperation()) return View(supplierViewModel);

            TempData["Sucesso"] = $"O Fornecedor {supplierViewModel.Name.ToUpper()} foi incluido com sucesso!";

            return RedirectToAction("Index");
        }

        [Route("supplier-edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var supplierViewModel = await GetSupplierAndAddressSupplierAndProducts(id);

            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

        [Route("supplier-edit/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id) return NotFound();

            supplierViewModel.Address = _mapper.Map<AddressViewModel>(await _supplierRepository.GetAddressBySupplierId(id));

            if (!ModelState.IsValid) return View(supplierViewModel);

            var supplier = _mapper.Map<Supplier>(supplierViewModel);
            await _supplierService.Update(supplier);

            if (!ValidOperation()) return View(await GetSupplierAndAddressSupplierAndProducts(id));

            TempData["Sucesso"] = $"O Fornecedor {supplierViewModel.Name.ToUpper()} foi alterado com sucesso!";

            return RedirectToAction("Index");
        }

        [Route("supplier-delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplierViewModel = await GetAddressSupplier(id);

            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

        [Route("supplier-delete/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplier = await GetAddressSupplier(id);

            if (supplier == null) return NotFound();

            await _supplierService.Remove(id);

            if (!ValidOperation()) return View(supplier);

            TempData["Sucesso"] = $"O Fornecedor {supplier.Name.ToUpper()} foi excluido com sucesso!";

            return RedirectToAction("Index");
        }

        private async Task<SupplierViewModel> GetAddressSupplier(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierAndAddress(id));
        }

        private async Task<SupplierViewModel> GetSupplierAndAddressSupplierAndProducts(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierAndProductsAndAddress(id));
        }
    }
}
