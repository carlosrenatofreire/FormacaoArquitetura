using Architecture.Business.Interfaces.Internals.Entities;
using Architecture.Business.Models.Internals.Entities;
using Architecture.Business.Models.Internals.Parameters;
using Architecture.Data.Contexts;
using Architecture.Data.Repositories.Shareds;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Data.Repositories.Entities
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(AppDbContext context) : base(context) { }

        public async Task<Address> GetAddressBySupplierId(Guid supplierId)
        {
            return await Db.Addresses.AsNoTracking()
                 .FirstOrDefaultAsync(a => a.SupplierId == supplierId);
        }

        public async Task<Supplier> GetSupplierAndAddress(Guid id)
        {
            return await Db.Suppliers.AsNoTracking()
                .Include(s => s.Address)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Supplier> GetSupplierAndProductsAndAddress(Guid id)
        {
            return await Db.Suppliers.AsNoTracking()
                .Include(s => s.Products)
                .Include(s => s.Address)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task RemoveSupplierAddress(Address address)
        {
            Db.Addresses.Remove(address);
            await SaveChanges();
        }
    }
}
