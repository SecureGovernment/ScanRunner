using SecureGovernment.Domain.Interfaces.Repositories;
using SecureGovernment.Domain.Interfaces.Services;

namespace SecureGovernment.Domain.Services
{
    public class WebsiteService : IWebsiteService
    {
        public IWebsiteRepository WebsiteRepository { get; set; }

        public int GetNextId() => WebsiteRepository.GetLargestId() + 1;
    }
}
