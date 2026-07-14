using Architecture.API.Configurations;
using Architecture.Data.Contexts;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

// 1. Usamos o gerador nativo do .NET 10 (Substitui o AddSwaggerGen)
builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperConfig>();
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.ResolveDependencies();

var app = builder.Build();

// Fixa a cultura em pt-PT (euro, vírgula decimal) para o binding e a formatação de decimais
// serem sempre consistentes, independentemente da cultura da máquina onde a API corre.
var ptPt = new[] { new CultureInfo("pt-PT") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-PT"),
    SupportedCultures = ptPt,
    SupportedUICultures = ptPt
});

if (app.Environment.IsDevelopment())
{
    // 2. Mapeia o endpoint do JSON nativo (/openapi/v1.json)
    app.MapOpenApi();

    // 3. Ativa o Swagger UI clássico apontando para o JSON correto do .NET 10
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Architecture API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

