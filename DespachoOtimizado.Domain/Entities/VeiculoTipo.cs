using DespachoOtimizado.Domain.Abstractions;

namespace DespachoOtimizado.Domain.Entities
{
    public class VeiculoTipo : EntityBaseWithName<byte>
    {
        public ICollection<Veiculo> Veiculos {get; protected set; } 
    }
}