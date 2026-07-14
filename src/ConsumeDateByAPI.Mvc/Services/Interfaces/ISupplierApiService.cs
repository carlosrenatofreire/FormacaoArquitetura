using ConsumeDateByAPI.Mvc.ViewModels;

namespace ConsumeDateByAPI.Mvc.Services.Interfaces
{
    public interface ISupplierApiService
    {
        Task<IEnumerable<SupplierViewModel>> GetAllAsync();
        Task<SupplierViewModel?> GetByIdAsync(Guid id);
        Task<ApiResult<SupplierViewModel>> AddAsync(SupplierViewModel supplier);
        Task<ApiResult<SupplierViewModel>> UpdateAsync(Guid id, SupplierViewModel supplier);
        Task<ApiResult> RemoveAsync(Guid id);
    }
}
