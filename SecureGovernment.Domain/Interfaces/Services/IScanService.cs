using System.Threading.Tasks;

namespace SecureGovernment.Domain.Interfaces.Services
{
    public interface IScanService
    {
        Task<int> TriggerScan(string hostname);
    }
}
