using SecureGovernment.Domain.Interfaces.Infastructure;

namespace SecureGovernment.Data.Infastructure
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork();
        }
    }
}
