using ConsumeDateByAPI.Mvc.Services.Interfaces;
using ConsumeDateByAPI.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConsumeDateByAPI.Mvc.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierApiService _supplierApiService;

        public SupplierController(ISupplierApiService supplierApiService)
        {
            _supplierApiService = supplierApiService;
        }

        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierApiService.GetAllAsync();
            return View(suppliers);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var supplier = await _supplierApiService.GetByIdAsync(id);

            if (supplier == null) return NotFound();

            return View(supplier);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new SupplierViewModel { Address = new AddressViewModel() });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid) return View(supplierViewModel);

            var result = await _supplierApiService.AddAsync(supplierViewModel);

            if (!result.Success)
            {
                AddErrors(result.Errors);
                return View(supplierViewModel);
            }

            TempData["Sucesso"] = $"O Fornecedor {supplierViewModel.Name.ToUpper()} foi incluido com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var supplier = await _supplierApiService.GetByIdAsync(id);

            if (supplier == null) return NotFound();

            return View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id) return NotFound();

            // A API não valida/actualiza o endereço no Update - mantemos os dados existentes para a partial de leitura.
            var existing = await _supplierApiService.GetByIdAsync(id);
            supplierViewModel.Address = existing?.Address;
            supplierViewModel.Products = existing?.Products;

            if (!ModelState.IsValid) return View(supplierViewModel);

            var result = await _supplierApiService.UpdateAsync(id, supplierViewModel);

            if (!result.Success)
            {
                AddErrors(result.Errors);
                return View(supplierViewModel);
            }

            TempData["Sucesso"] = $"O Fornecedor {supplierViewModel.Name.ToUpper()} foi alterado com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var supplier = await _supplierApiService.GetByIdAsync(id);

            if (supplier == null) return NotFound();

            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplier = await _supplierApiService.GetByIdAsync(id);

            if (supplier == null) return NotFound();

            var result = await _supplierApiService.RemoveAsync(id);

            if (!result.Success)
            {
                AddErrors(result.Errors);
                return View(supplier);
            }

            TempData["Sucesso"] = $"O Fornecedor {supplier.Name.ToUpper()} foi excluido com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        private void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}
