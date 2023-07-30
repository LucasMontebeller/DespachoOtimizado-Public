using DespachoOtimizado.Domain.Abstractions;

namespace DespachoOtimizado.Domain.Entities
{
    public class Veiculo : EntityBaseWithName<int>
    {
        public byte VeiculoTipoId { get; private set; }
        public byte Quantidade { get; private set; }
        public Decimal Capacidade { get; private set; }

        // Navegação
        public VeiculoTipo VeiculoTipo { get; private set; }
    }
}