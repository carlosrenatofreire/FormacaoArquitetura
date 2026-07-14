namespace ConsumeDateByAPI.Mvc.ViewModels
{
    // Espelha o formato devolvido por BaseController.CustomResponse na API:
    // BadRequest(new { errors = notifications.Select(n => n.Message) })
    public class ApiErrorResponse
    {
        public List<string> Errors { get; set; } = new();
    }
}
