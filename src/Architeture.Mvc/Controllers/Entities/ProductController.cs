using Architecture.Business.Interfaces.Internals.Entities;
using Architecture.Business.Interfaces.Shareds;
using Architecture.Business.Models.Internals.Entities;
using Architeture.Mvc.Controllers.Shareds;
using Architeture.Mvc.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Architeture.Mvc.Controllers.Entities
{
    public class ProductController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly ISupplierRepository _supplierRepository;

        public ProductController(IMapper mapper,
                                 IProductRepository productRepository,
                                 ISupplierRepository supplierRepository,
                                 IProductService productService,
                                 INotifierService notifier) : base(notifier)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productService = productService;
            _supplierRepository = supplierRepository;
        }

        [Route("product-list")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsAndSuppliers()));
        }

        [Route("product-details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [Route("product-new")]
        public async Task<IActionResult> Create()
        {
            var productViewModel = await PopulateSupplierData(new ProductViewModel());

            return View(productViewModel);
        }

        [Route("product-new")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await PopulateSupplierData(productViewModel);

            if (!ModelState.IsValid) return View(productViewModel);

            await _productService.Add(_mapper.Map<Product>(productViewModel));

            if (!ValidOperation()) return View(productViewModel);

            TempData["Sucesso"] = $"O produto {productViewModel.Name.ToUpper()} foi incluido com sucesso!";

            return RedirectToAction("Index");
        }


        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.GetProductAndSupplier(id));
            product.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAll());
            return product;
        }

        private async Task<ProductViewModel> PopulateSupplierData(ProductViewModel product)
        {
            product.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAll());
            return product;
        }
    }
}
