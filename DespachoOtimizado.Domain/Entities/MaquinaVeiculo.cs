namespace DespachoOtimizado.Domain.Entities
{
    public class MaquinaVeiculo : EntityBase<int>
    {
        public int MaquinaId { get; private set; }
        public int VeiculoId { get; private set; }

        public Maquina Maquina { get; private set; }
        public Veiculo Veiculo { get; private set; }
    }
}