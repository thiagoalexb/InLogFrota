using System.ComponentModel.DataAnnotations;

namespace InLogFrota.Data.Enums
{
    public enum TipoVeiculo
    {
        [Display(Name = "Caminhão")]
        Caminhao = 2,
        [Display(Name = "Ônibus")]
        Onibus = 42
    }
}
