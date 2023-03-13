namespace NLayer.Web.Models
{
    public abstract class BaseCollectionViewModel<T>
    {
        public IReadOnlyCollection<T> Items { get; set; }
    }
}
