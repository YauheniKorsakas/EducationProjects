using Azure.Core;
using Microsoft.EntityFrameworkCore;
using NLayer.Domain.Base;
using System.Linq.Expressions;
using System.Xml;

namespace NLayer.Infrastructure.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
                                                                        where TKey : struct
    {
        protected readonly ShopContext context;
        protected readonly DbSet<TEntity> entities;

        public Repository(ShopContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            entities = this.context.Set<TEntity>();
        }

        public void Add(TEntity entity) {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entities.Add(entity);
        }

        public void Delete(TEntity entity) {
            if (context.Entry(entity).State == EntityState.Detached) {
                entities.Attach(entity);
            }
            entities.Remove(entity);
        }

        public void Delete(TKey key) {
            var existing = Get(key); // shit - would be better without retreiving.
            Delete(existing);
        }

        public TEntity Get(TKey id) {
            return entities.Find(id);
        }

        public IEnumerable<TEntity> Get() {
            return entities.ToList();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate) {
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));
            return entities.Where(predicate).AsEnumerable();
        }

        public void Update(TEntity entity) {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] updatableProperties) {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            if (updatableProperties is null) throw new ArgumentException(nameof(updatableProperties));

            context.Attach(entity);

            foreach (var prop in updatableProperties) {
                context.Entry(entity).Property(prop).IsModified = true;
            }
        }

        public Task SaveAsync() => context.SaveChangesAsync();
    }

    public class Repository<TEntity> : Repository<TEntity, int>, IRepository<TEntity> where TEntity : BaseEntity<int> {
        public Repository(ShopContext context) : base(context) { }
    }
}
