using ConsumeDateByAPI.Mvc.Services.Interfaces;
using ConsumeDateByAPI.Mvc.ViewModels;
using System.Net;
using System.Net.Http.Json;

namespace ConsumeDateByAPI.Mvc.Services
{
    public class ProductApiService : ApiServiceBase, IProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductViewModel>>("api/Product", JsonOptions);
            return products ?? Enumerable.Empty<ProductViewModel>();
        }

        public async Task<ProductViewModel?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Product/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductViewModel>(JsonOptions);
        }

        public async Task<IEnumerable<ProductViewModel>> GetBySupplierIdAsync(Guid supplierId)
        {
            var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductViewModel>>($"api/Product/supplier/{supplierId}", JsonOptions);
            return products ?? Enumerable.Empty<ProductViewModel>();
        }

        public async Task<ApiResult<ProductViewModel>> AddAsync(ProductViewModel product)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Product", product, JsonOptions);
            return await BuildResultAsync<ProductViewModel>(response);
        }

        public async Task<ApiResult<ProductViewModel>> UpdateAsync(Guid id, ProductViewModel product)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Product/{id}", product, JsonOptions);
            return await BuildResultAsync<ProductViewModel>(response);
        }

        public async Task<ApiResult> RemoveAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Product/{id}");
            return await BuildResultAsync(response);
        }
    }
}
