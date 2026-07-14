using ConsumeDateByAPI.Mvc.ViewModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace ConsumeDateByAPI.Mvc.Services
{
    public abstract class ApiServiceBase
    {
        protected static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        protected static async Task<ApiResult<T>> BuildResultAsync<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.Headers.ContentLength > 0
                    ? await response.Content.ReadFromJsonAsync<T>(JsonOptions)
                    : default;

                return ApiResult<T>.Ok(data);
            }

            return ApiResult<T>.Fail(await ReadErrorsAsync(response));
        }

        protected static async Task<ApiResult> BuildResultAsync(HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode
                ? ApiResult.Ok()
                : ApiResult.Fail(await ReadErrorsAsync(response));
        }

        private static async Task<List<string>> ReadErrorsAsync(HttpResponseMessage response)
        {
            if (response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                var payload = await response.Content.ReadFromJsonAsync<ApiErrorResponse>(JsonOptions);
                if (payload?.Errors is { Count: > 0 })
                {
                    return payload.Errors;
                }
            }

            return new List<string> { $"Erro ao comunicar com a API ({(int)response.StatusCode} {response.ReasonPhrase})" };
        }
    }
}
