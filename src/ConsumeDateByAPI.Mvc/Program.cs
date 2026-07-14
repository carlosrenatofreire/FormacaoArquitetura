using ConsumeDateByAPI.Mvc.Services;
using ConsumeDateByAPI.Mvc.Services.Interfaces;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"]
    ?? throw new InvalidOperationException("ApiSettings:BaseUrl não configurado em appsettings.json");

// HttpClients tipados (IHttpClientFactory) - uma instância de HttpClient por serviço,
// com o ciclo de vida e pooling de conexões geridos pela framework.
builder.Services.AddHttpClient<ISupplierApiService, SupplierApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<IProductApiService, ProductApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

var app = builder.Build();

// Fixa a cultura em pt-PT (euro, vírgula decimal) para o binding e a formatação de decimais
// serem sempre consistentes, independentemente da cultura da máquina onde a app corre.
var ptPt = new[] { new CultureInfo("pt-PT") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-PT"),
    SupportedCultures = ptPt,
    SupportedUICultures = ptPt
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
