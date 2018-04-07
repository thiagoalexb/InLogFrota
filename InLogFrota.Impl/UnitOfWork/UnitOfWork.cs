using InLogFrota.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace InLogFrota.Impl.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private Hashtable repositories;

        public UnitOfWork(DbContext context) =>
            _context = context;

        public async Task<bool> Complete()
        {
            var isSaved = await _context.SaveChangesAsync() > 0;
            return isSaved;
        }

        public TEntity Repository<TEntity>() where TEntity : class
        {
            if (repositories == null)
            {
                repositories = new Hashtable();
            }

            var type = typeof(TEntity);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);

            foreach (var item in types)
            {
                if (!repositories.ContainsKey(item))
                {
                    repositories.Add(type, Activator.CreateInstance(item, _context));
                }
            }

            return (TEntity)repositories[type];
        }

        public void Dispose() =>
            _context.Dispose();

        
    }
}
