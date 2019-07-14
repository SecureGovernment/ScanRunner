using SecureGovernment.Domain.Dtos;
using System.Collections.Generic;

namespace SecureGovernment.Domain.Interfaces.Repositories
{
    public interface IWebsiteRepository
    {
        IDictionary<string, bool> DoWebsitesExist(IList<string> hostName);
        void Add(WebsiteDto website);
        int GetLargestId();
        IList<WebsiteDto> GetNextScanTargets(int numberOfTargets);
        void UpdateLastScanDateToNow(int id);
    }
}
