using InLogFrota.Core.Notifications;
using InLogFrota.Core.UnitOfWork;
using InLogFrota.Impl.Notifications;
using System.Threading.Tasks;

namespace InLogFrota.Impl.Services.Base
{
    public abstract class Service<TRepository> where TRepository : class
    {
        protected IMediatorHandler _bus;
        protected IUnitOfWork _unitOfWork;
        protected TRepository _repository;

        public Service(IMediatorHandler bus,
                       IUnitOfWork unitOfWork)
        {
            _bus = bus;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<TRepository>();
        }

        protected async Task<bool> Commit()
        {
            if (!await _unitOfWork.Complete())
            {
                await _bus.RaiseEvent(new Notification("Erro ao salvar, tente novamente."));
                return false;
            }
            return true;
        }
    }
}
