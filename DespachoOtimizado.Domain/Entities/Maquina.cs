using DespachoOtimizado.Domain.Abstractions;

namespace DespachoOtimizado.Domain.Entities
{
    public class Maquina : EntityBaseWithName<int>
    {
        public byte Quantidade { get; private set; } 
    }
}