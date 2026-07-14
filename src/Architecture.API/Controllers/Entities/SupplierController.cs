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
    [Route("api/[controller]")] // Define a rota base da controller (ex: api/supplier)
    public class SupplierController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierService _supplierService;

        public SupplierController(IMapper mapper,
                                  ISupplierRepository supplierRepository,
                                  ISupplierService supplierService,
                                  INotifierService notifier) : base(notifier)
        {
            _mapper = mapper;
            _supplierRepository = supplierRepository;
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IEnumerable<SupplierViewModel>> GetAll()
        {
            var suppliers = await _supplierRepository.GetAllSuppliersAndProductsAndAddresses();
            return _mapper.Map<List<SupplierViewModel>>(suppliers);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> GetById(Guid id)
        {
            var fornecedor = await GetSupplierProductAddress(id);

            if (fornecedor == null) return NotFound();

            return fornecedor;
        }

        [HttpPost]
        public async Task<ActionResult<SupplierViewModel>> Add(SupplierViewModel SupplierViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _supplierService.Add(_mapper.Map<Supplier>(SupplierViewModel));

            return CustomResponse(HttpStatusCode.Created, SupplierViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> Update(Guid id, SupplierViewModel SupplierViewModel)
        {
            if (id != SupplierViewModel.Id)
            {
                NotifierError("O id informado não é o mesmo que foi passado na query");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _supplierService.Update(_mapper.Map<Supplier>(SupplierViewModel));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> Delete(Guid id)
        {
            await _supplierService.Remove(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        private async Task<SupplierViewModel> GetSupplierProductAddress(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierAndProductsAndAddress(id));
        }
    }
}
