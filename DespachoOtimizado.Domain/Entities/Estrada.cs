using DespachoOtimizado.Domain.Abstractions;

namespace DespachoOtimizado.Domain.Entities
{
    public class Estrada : EntityBaseWithName<int>
    {
        public Decimal VelocidadeMedia { get; private set; }
        public byte EstradaTipoId { get; private set; }
        public byte EstradaSubTipoId { get; private set; }

        public EstradaTipo EstradaTipo { get; private set; }
        public EstradaSubTipo EstradaSubTipo { get; private set; }
    }
}