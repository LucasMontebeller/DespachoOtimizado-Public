using DespachoOtimizado.Domain.Entities;

namespace DespachoOtimizado.Domain.Abstractions
{
    public abstract class EntityBaseWithName<T> : EntityBase<T>
    {
        public string Nome { get; protected set; }
    }
}