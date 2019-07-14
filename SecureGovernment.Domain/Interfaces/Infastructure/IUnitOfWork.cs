using System;

namespace SecureGovernment.Domain.Interfaces.Infastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Queue(ICommand command);
    }
}
