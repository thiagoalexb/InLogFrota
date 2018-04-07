using AutoMapper;
using InLogFrota.Core.ViewModels;
using InLogFrota.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLogFrota.ApresentationWeb.Mapper
{
    class ViewModelToEntityProfile : Profile
    {
        public ViewModelToEntityProfile()
        {
            CreateMap<VeiculoViewModel, Veiculo>();
        }
    }
}
