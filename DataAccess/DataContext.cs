using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess
{
    public class DataContext(AppDbContext contentDistributionSystemContext) : IDataContext
    {
        private readonly AppDbContext appDbContext = contentDistributionSystemContext;

        public void Create<TEntity>(TEntity entity)
            where TEntity : BaseEntity
        {
            appDbContext.Set<TEntity>().Add(entity);
        }

        public IQueryable<TEntity> Get<TEntity>()
            where TEntity : BaseEntity
        {
            return appDbContext.Set<TEntity>();
        }

        public void Remove<TEntity>(TEntity entity)
            where TEntity : BaseEntity
        {
            appDbContext.Set<TEntity>().Remove(entity);
        }

        public void Update<TEntity>(TEntity entity)
            where TEntity : BaseEntity
        {
            appDbContext.Set<TEntity>().Update(entity);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await appDbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
