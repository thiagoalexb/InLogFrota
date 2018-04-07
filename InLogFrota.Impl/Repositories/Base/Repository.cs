using InLogFrota.Core.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InLogFrota.Impl.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() => 
            await DbSet.ToListAsync();

        public virtual async Task<TEntity> GetAsync(Guid id) =>
            await DbSet.FindAsync(id);
        
        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) =>
            await DbSet.Where(predicate).ToListAsync();

        public virtual async Task<TEntity> GetByCriteriaAsync(Expression<Func<TEntity, bool>> predicate) =>
            await DbSet.FirstOrDefaultAsync(predicate);
        
        public virtual async Task AddAsync(TEntity entity) =>
            await DbSet.AddAsync(entity);

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities) =>
            await DbSet.AddRangeAsync(entities);

        public async Task RemoveAsync(TEntity entity) =>
            await Task.FromResult(DbSet.Remove(entity));
    }
}
