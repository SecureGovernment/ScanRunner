using System;
using System.Collections.Generic;
using System.Text;

namespace SecureGovernment.Domain.Interfaces.Infastructure
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork GetUnitOfWork();
    }
}
