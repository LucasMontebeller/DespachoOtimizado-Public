namespace DespachoOtimizado.Domain.Entities
{
    public abstract class EntityBase<T>
    {
        public T Id { get; protected set; }
    }
}