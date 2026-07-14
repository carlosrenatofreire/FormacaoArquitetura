using ConsumeDateByAPI.Mvc.Services.Interfaces;
using ConsumeDateByAPI.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConsumeDateByAPI.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiService _productApiService;
        private readonly ISupplierApiService _supplierApiService;

        public ProductController(IProductApiService productApiService, ISupplierApiService supplierApiService)
        {
            _productApiService = productApiService;
            _supplierApiService = supplierApiService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productApiService.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productApiService.GetByIdAsync(id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var product = await PopulateSuppliersAsync(new ProductViewModel());
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                await PopulateSuppliersAsync(productViewModel);
                return View(productViewModel);
            }

            var result = await _productApiService.AddAsync(productViewModel);

            if (!result.Success)
            {
                AddErrors(result.Errors);
                await PopulateSuppliersAsync(productViewModel);
                return View(productViewModel);
            }

            TempData["Sucesso"] = $"O produto {productViewModel.Name.ToUpper()} foi incluido com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productApiService.GetByIdAsync(id);

            if (product == null) return NotFound();

            await PopulateSuppliersAsync(product);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                await PopulateSuppliersAsync(productViewModel);
                return View(productViewModel);
            }

            var result = await _productApiService.UpdateAsync(id, productViewModel);

            if (!result.Success)
            {
                AddErrors(result.Errors);
                await PopulateSuppliersAsync(productViewModel);
                return View(productViewModel);
            }

            TempData["Sucesso"] = $"O produto {productViewModel.Name.ToUpper()} foi alterado com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productApiService.GetByIdAsync(id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _productApiService.GetByIdAsync(id);

            if (product == null) return NotFound();

            var result = await _productApiService.RemoveAsync(id);

            if (!result.Success)
            {
                AddErrors(result.Errors);
                return View(product);
            }

            TempData["Sucesso"] = $"O produto {product.Name.ToUpper()} foi excluido com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        private async Task<ProductViewModel> PopulateSuppliersAsync(ProductViewModel product)
        {
            product.Suppliers = await _supplierApiService.GetAllAsync();
            return product;
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
