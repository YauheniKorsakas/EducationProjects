using Microsoft.EntityFrameworkCore;
using NLayer.Domain.Base;
using System.Linq.Expressions;

namespace NLayer.Infrastructure.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
                                                                        where TKey : struct
    {
        public IQueryable<TEntity> Query { get; }

        protected readonly ShopContext context;
        protected readonly DbSet<TEntity> entities;

        public Repository(ShopContext context) {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            entities = this.context.Set<TEntity>();
            Query = entities.AsQueryable();
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
            var existing = context.Find<TEntity>(key);
            Delete(existing);
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

    public class Repository<TEntity> : Repository<TEntity, int>, IRepository<TEntity> where TEntity : BaseEntity<int>
    {
        public Repository(ShopContext context) : base(context) { }
    }
}
