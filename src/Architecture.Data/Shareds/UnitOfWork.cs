using Architecture.Business.Interfaces.Shareds;
using Architecture.Data.Contexts;

namespace Architecture.Data.Shareds
{
    // Mesma instância de AppDbContext dos repositórios (escopo por pedido HTTP via DI),
    // por isso um único Commit() persiste tudo o que os repositórios acumularam no ChangeTracker.
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Commit()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
