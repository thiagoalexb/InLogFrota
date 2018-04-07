using InLogFrota.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace InLogFrota.Core.ViewModels
{
    public class VeiculoViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Chassi é obrigatório!")]
        public string Chassi { get; set; }
        public TipoVeiculo Tipo { get; set; }

        [Required(ErrorMessage = "Cor é obrigatório!")]
        public string Cor { get; set; }
        public int NumeroPassageiros { get; set; }
        public bool Deleted { get; set; }
    }
}
