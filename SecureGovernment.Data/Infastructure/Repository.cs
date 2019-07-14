using Microsoft.EntityFrameworkCore;
using SecureGovernment.Domain.Interfaces.Infastructure;

namespace SecureGovernment.Data.Infastructure
{
    public abstract class Repository<TDataContext, TEntity> : IRepository<TEntity>
            where TDataContext : DbContext
            where TEntity : class
    {
        protected TDataContext DataContext { get; }
        protected DbSet<TEntity> DbSet { get { return DataContext.Set<TEntity>(); } }

        public Repository(IDatabaseFactory factory)
        {
            this.DataContext = this.GetContext(factory);
        }

        protected abstract TDataContext GetContext(IDatabaseFactory factory);

    }
}
