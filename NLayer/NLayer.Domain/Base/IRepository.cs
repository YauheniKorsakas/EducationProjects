using System.Linq.Expressions;

namespace NLayer.Domain.Base
{
    public interface IRepository<T, U> where T : BaseEntity<U>
                                       where U : struct
    {
        T Get(U id);
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
