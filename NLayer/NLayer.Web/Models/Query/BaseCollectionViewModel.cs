namespace NLayer.Web.Models.Query
{
    public class BaseCollectionViewModel<T>
    {
        public IReadOnlyCollection<T> Items { get; set; }
    }
}
