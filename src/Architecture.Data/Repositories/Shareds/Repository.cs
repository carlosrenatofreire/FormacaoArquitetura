using Architecture.Business.Interfaces.Shareds;
using Architecture.Business.Models.Shareds;
using Architecture.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Architecture.Data.Repositories.Shareds
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(AppDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        // Metodos de leitura
        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        // Metodos de persistencia
        // Apenas marcam o estado no ChangeTracker - quem grava é o IUnitOfWork.Commit(),
        // para que várias operações do mesmo caso de uso sejam persistidas atomicamente.

        public Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            return Task.CompletedTask;
        }

        public Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task Remove(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
