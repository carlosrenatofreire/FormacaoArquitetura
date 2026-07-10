using Architecture.Business.Interfaces.Internals.Entities;
using Architecture.Business.Interfaces.Shareds;
using Architeture.Mvc.Controllers.Shareds;
using Architeture.Mvc.ViewModels;
using AutoMapper;
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

        [Route("suppler-list")]
        public async Task<IActionResult> Index()
        {
            var suppliers = await _supplierRepository.GetAll();
            var mappedSuppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
            return View(mappedSuppliers);
        }
    }
}
