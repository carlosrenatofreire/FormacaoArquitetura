using ConsumeDateByAPI.Mvc.ViewModels;

namespace ConsumeDateByAPI.Mvc.Services.Interfaces
{
    public interface IProductApiService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();
        Task<ProductViewModel?> GetByIdAsync(Guid id);
        Task<IEnumerable<ProductViewModel>> GetBySupplierIdAsync(Guid supplierId);
        Task<ApiResult<ProductViewModel>> AddAsync(ProductViewModel product);
        Task<ApiResult<ProductViewModel>> UpdateAsync(Guid id, ProductViewModel product);
        Task<ApiResult> RemoveAsync(Guid id);
    }
}
