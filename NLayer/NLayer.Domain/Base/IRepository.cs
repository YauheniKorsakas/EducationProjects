using System.Linq.Expressions;

namespace NLayer.Domain.Base
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
                                                where TKey : struct
    {
        TEntity Get(TKey id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TKey key);
        void Update(TEntity entity);
        Task SaveAsync();
    }

    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity: BaseEntity<int> { }
}
