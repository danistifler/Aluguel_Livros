using DevIO.Business.Core.Data;
using DevIO.Business.Core.Models;
using DevIO.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Infra.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {

        protected readonly MeuDbContext Db;
        protected readonly DbSet<T> DbSet;

        protected Repository()
        {
            Db = new MeuDbContext();
            DbSet = Db.Set<T>();

        }
        public virtual async Task<T> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<T>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
                
        }

        public virtual async Task Adicionar(T entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            Db.Entry(new T {Id = id }).State = EntityState.Deleted;
            await SaveChanges();

        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
