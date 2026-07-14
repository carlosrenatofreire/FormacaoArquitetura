using Architecture.Data.Contexts;
using Architeture.Mvc.Configurations;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperConfig>();
});

builder.Services.AddControllersWithViews();

builder.Services.ResolveDependencies();

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

// Configure
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/error/500");
    app.UseStatusCodePagesWithRedirects("/error/{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
