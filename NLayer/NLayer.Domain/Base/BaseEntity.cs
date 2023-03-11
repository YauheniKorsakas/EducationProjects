namespace NLayer.Domain.Base
{
    public abstract class BaseEntity<T> where T: struct
    {
        public T Id { get; protected set; }
    }
}
