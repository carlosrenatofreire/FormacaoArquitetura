using Architecture.Business.Interfaces.Internals.Entities;
using Architecture.Business.Models.Internals.Entities;
using Architecture.Data.Contexts;
using Architecture.Data.Repositories.Shareds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.Data.Repositories.Entities
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public Task<Product> GetProductAndSupplier(Guid id)
        {
            var product = Db.Products.AsNoTracking()
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAndSuppliers()
        {
            return await Db.Products.AsNoTracking()
                .Include(p => p.Supplier)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBySupplierId(Guid supplierId)
        {
            return await Find(p => p.SupplierId == supplierId);
        }
    }
}