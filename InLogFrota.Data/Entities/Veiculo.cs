using InLogFrota.Data.Entities.Base;
using InLogFrota.Data.Enums;
using System;

namespace InLogFrota.Data.Entities
{
    public class Veiculo : EntityBase
    {
        public Veiculo(Guid id, string chassi, TipoVeiculo tipo, string cor)
        {
            Id = id;
            Chassi = chassi;
            Tipo = tipo;
            Cor = cor;
        }

        public Veiculo() { }

        public string Chassi { get; private set; }
        public TipoVeiculo Tipo { get; private set; }
        public string Cor { get; private set; }
        public int NumeroPassageiros { get; private set; }

        public void SetCor(string cor)
        {
            Cor = cor;
        }

        public void SetNumeroPassageiros()
        {
            switch (Tipo)
            {
                case TipoVeiculo.Caminhao:
                    NumeroPassageiros = 2;
                    break;
                case TipoVeiculo.Onibus:
                    NumeroPassageiros = 42;
                    break;
            }
        }
    }
}
