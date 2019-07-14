namespace SecureGovernment.Data.Infastructure
{
    public abstract class DatabaseRepository<TEntity> : Repository<ObservatoryContext, TEntity> where TEntity : class
    {
        public DatabaseRepository(IDatabaseFactory factory) : base(factory) { }

        protected override ObservatoryContext GetContext(IDatabaseFactory factory)
        {
            return factory.GetDatabase();
        }
    }
}
