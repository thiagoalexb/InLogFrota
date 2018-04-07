using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InLogFrota.Core.Notifications;
using InLogFrota.Core.Repositories;
using InLogFrota.Core.Services;
using InLogFrota.Core.UnitOfWork;
using InLogFrota.Data.Entities;
using InLogFrota.Impl.Notifications;
using InLogFrota.Impl.Services.Base;

namespace InLogFrota.Impl.Services
{
    public class VeiculoService : Service<IVeiculoRepository>, IVeiculoService
    {
        public VeiculoService(IMediatorHandler bus,
                            IUnitOfWork unitOfWork) : base(bus, unitOfWork) { }

        public async Task<IEnumerable<Veiculo>> GetAllAsync() => 
            await _repository.GetAllAsync();

        public async Task<Veiculo> GetAsync(Guid id) => 
            await _repository.GetAsync(id);

        public async Task<IEnumerable<Veiculo>> FindAsync(Expression<Func<Veiculo, bool>> predicate) => 
            await _repository.FindAsync(predicate);

        public async Task<Veiculo> GetByCriteriaAsync(Expression<Func<Veiculo, bool>> predicate) => 
            await _repository.GetByCriteriaAsync(predicate);

        public async Task AddAsync(Veiculo entity)
        {
            var veiculo = await _repository.GetByCriteriaAsync(x => x.Chassi == entity.Chassi);
            if(veiculo != null)
            {
                await _bus.RaiseEvent(new Notification("Este Chassi já está sendo usado"));
                return;
            }

            entity.SetNumeroPassageiros();
            entity.SetCreationDate();
            await _repository.AddAsync(entity);

            await Commit();
        }

        public async Task UpdateAsync(Veiculo entity)
        {
            var veiculo = await _repository.GetByCriteriaAsync(x => x.Id == entity.Id);
            if(veiculo == null)
            {
                await _bus.RaiseEvent(new Notification("Este veiculo não esta cadastrado no nosso banco de dados"));
                return;
            }

            if(veiculo.Cor != entity.Cor)
            {
                veiculo.SetCor(entity.Cor);
                veiculo.SetChangeDate();
                await Commit();
            } 
        }

        public async Task RemoveAsync(Veiculo entity)
        {
            var veiculo = await _repository.GetByCriteriaAsync(x => x.Id == entity.Id);
            if (veiculo == null)
            {
                await _bus.RaiseEvent(new Notification("Este veiculo não esta cadastrado no nosso banco de dados"));
                return;
            }

            await _repository.RemoveAsync(veiculo);

            await Commit();
        }

        
    }
}
