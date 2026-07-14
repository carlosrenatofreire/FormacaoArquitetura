namespace ConsumeDateByAPI.Mvc.Services
{
    // Resultado de uma chamada à API que pode falhar por validação de negócio (400 com "errors").
    // Evita usar exceções para o fluxo normal de validação - só HttpRequestException para falhas de infraestrutura.
    public class ApiResult
    {
        public bool Success { get; init; }
        public List<string> Errors { get; init; } = new();

        public static ApiResult Ok() => new() { Success = true };
        public static ApiResult Fail(IEnumerable<string> errors) => new() { Success = false, Errors = errors.ToList() };
    }

    public class ApiResult<T> : ApiResult
    {
        public T? Data { get; init; }

        public static ApiResult<T> Ok(T? data) => new() { Success = true, Data = data };
        public static new ApiResult<T> Fail(IEnumerable<string> errors) => new() { Success = false, Errors = errors.ToList() };
    }
}
