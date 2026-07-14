using Architecture.Business.Interfaces.Internals.Entities;
using Architecture.Business.Interfaces.Shareds;
using Architecture.Business.Services.Internals.Entities;
using Architecture.Business.Services.Shareds;
using Architecture.Data.Contexts;
using Architecture.Data.Repositories.Entities;
using Architecture.Data.Shareds;

namespace Architecture.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // Context
            services.AddScoped<AppDbContext>();

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();

            // Services
            services.AddScoped<INotifierService, NotifierService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
