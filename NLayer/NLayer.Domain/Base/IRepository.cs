using System.Linq.Expressions;

namespace NLayer.Domain.Base
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
                                                where TKey : struct
    {
        IQueryable<TEntity> Query { get; }
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TKey key);
        void Update(TEntity entity);
        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] updatableProperties);
        Task SaveAsync();
    }

    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity: BaseEntity<int> { }
}
