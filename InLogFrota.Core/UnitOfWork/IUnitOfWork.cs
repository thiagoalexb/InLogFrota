using System;
using System.Threading.Tasks;

namespace InLogFrota.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Complete();
        TEntity Repository<TEntity>() where TEntity : class;
    }
}
