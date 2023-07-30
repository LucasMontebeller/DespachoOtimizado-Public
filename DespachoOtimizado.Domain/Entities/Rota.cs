namespace DespachoOtimizado.Domain.Entities
{
    public class Rota : EntityBase<int>
    {
        public int OrigemId { get; private set; }
        public int DestinoId { get; private set; }
        public int RotaId { get; private set; }
        public Decimal Custo { get; private set; }
        public Decimal Tempo { get; private set; }

        public Localizacao Origem { get; private set; }
        public Localizacao Destino { get; private set; }
        public Rota RotaNavegation { get; private set; }
        public ICollection<Rota> Rotas { get; private set; }

    }
}