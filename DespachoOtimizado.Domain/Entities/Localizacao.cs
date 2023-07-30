using DespachoOtimizado.Domain.Abstractions;

namespace DespachoOtimizado.Domain.Entities
{
    public class Localizacao : EntityBaseWithName<int>
    {
        public Decimal Latitude { get; private set; }
        public Decimal Longitude { get; private set;}
        public bool Garagem { get; private set; }
    }
}