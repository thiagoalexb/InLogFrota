using InLogFrota.Core.Repositories;
using InLogFrota.Data.Entities;
using InLogFrota.Impl.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace InLogFrota.Impl.Repositories
{
    public class VeiculoRepository : Repository<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository(DbContext context) : base(context)
        { }
    }
}
