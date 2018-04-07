using AutoMapper;
using InLogFrota.Core.ViewModels;
using InLogFrota.Data.Entities;

namespace InLogFrota.ApresentationWeb.Mapper
{
    public class EntityToViewModelProfile : Profile
    {
        public EntityToViewModelProfile()
        {
            CreateMap<Veiculo, VeiculoViewModel>();
        }
    }
}
