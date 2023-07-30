using DespachoOtimizado.Domain.Abstractions;

namespace DespachoOtimizado.Domain.Entities
{
    public class MaquinaVeiculoRota : EntityBaseWithName<int>
    {
        public int MaquinaVeiculoId { get; private set; } 
        public int RotaId { get; private set; }

        public MaquinaVeiculo MaquinaVeiculo { get; private set; }
        public Rota Rota { get; private set; }
    }
}