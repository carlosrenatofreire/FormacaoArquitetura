using ConsumeDateByAPI.Mvc.Services.Interfaces;
using ConsumeDateByAPI.Mvc.ViewModels;
using System.Net;
using System.Net.Http.Json;

namespace ConsumeDateByAPI.Mvc.Services
{
    public class SupplierApiService : BaseApiService, ISupplierApiService
    {
        private readonly HttpClient _httpClient;

        public SupplierApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<SupplierViewModel>> GetAllAsync()
        {
            var suppliers = await _httpClient.GetFromJsonAsync<IEnumerable<SupplierViewModel>>("api/Supplier", JsonOptions);
            return suppliers ?? Enumerable.Empty<SupplierViewModel>();
        }

        public async Task<SupplierViewModel?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Supplier/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SupplierViewModel>(JsonOptions);
        }

        public async Task<ApiResult<SupplierViewModel>> AddAsync(SupplierViewModel supplier)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Supplier", supplier, JsonOptions);
            return await BuildResultAsync<SupplierViewModel>(response);
        }

        public async Task<ApiResult<SupplierViewModel>> UpdateAsync(Guid id, SupplierViewModel supplier)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Supplier/{id}", supplier, JsonOptions);
            return await BuildResultAsync<SupplierViewModel>(response);
        }

        public async Task<ApiResult> RemoveAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Supplier/{id}");
            return await BuildResultAsync(response);
        }
    }
}
