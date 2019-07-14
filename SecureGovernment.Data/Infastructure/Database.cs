using ScanRunner.Data;
using System;

namespace SecureGovernment.Data.Infastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        ObservatoryContext GetDatabase();
    }

    public class DatabaseFactory : IDatabaseFactory
    {
        private bool _disposed;

        private Lazy<ObservatoryContext> _database { get; set; }

        public DatabaseFactory() { _database = new Lazy<ObservatoryContext>(); }
        public DatabaseFactory(ObservatoryContext context) { _database = new Lazy<ObservatoryContext>(() => { return context; }); }

        public ObservatoryContext GetDatabase()
        {
            return _database.Value;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                if (disposing)
                {
                    if (_database != null && _database.IsValueCreated) _database.Value.Dispose();
                }

                _database = null;
                _disposed = true;
            }
        }

        ~DatabaseFactory()
        {
            Dispose(false);
        }
    }
}
