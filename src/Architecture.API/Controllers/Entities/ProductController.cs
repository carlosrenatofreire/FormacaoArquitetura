using Architecture.API.Controllers.Shareds;
using Architecture.API.ViewModels;
using Architecture.Business.Interfaces.Internals.Entities;
using Architecture.Business.Interfaces.Shareds;
using Architecture.Business.Models.Internals.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Architecture.API.Controllers.Entities
{
    [Route("api/[controller]")] // Define a rota base da controller (ex: api/product)
    public class ProductController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;

        public ProductController(IMapper mapper,
                                  IProductRepository productRepository,
                                  IProductService productService,
                                  INotifierService notifier) : base(notifier)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            var products = await _productRepository.GetProductsAndSuppliers();
            return _mapper.Map<List<ProductViewModel>>(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> GetById(Guid id)
        {
            var product = await GetProductAndSupplier(id);

            if (product == null) return NotFound();

            return product;
        }

        [HttpGet("supplier/{supplierId:guid}")]
        public async Task<IEnumerable<ProductViewModel>> GetBySupplierId(Guid supplierId)
        {
            var products = await _productRepository.GetProductsBySupplierId(supplierId);
            return _mapper.Map<List<ProductViewModel>>(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> Add(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _productService.Add(_mapper.Map<Product>(productViewModel));

            return CustomResponse(HttpStatusCode.Created, productViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> Update(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                NotifierError("O id informado não é o mesmo que foi passado na query");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _productService.Update(_mapper.Map<Product>(productViewModel));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> Delete(Guid id)
        {
            await _productService.Remove(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        private async Task<ProductViewModel> GetProductAndSupplier(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetProductAndSupplier(id));
        }
    }
}
