using SecureGovernment.Domain.Exceptions;
using SecureGovernment.Domain.Interfaces.Facades;
using SecureGovernment.Domain.Interfaces.Repositories;
using SecureGovernment.Domain.Interfaces.Services;
using System.Linq;

namespace SecureGovernment.Domain.Facades
{
    public class ScanFacade : IScanFacade
    {
        public IScanService ScanService { get; set; }
        public IWebsiteRepository WebsiteRepository { get; set; }
        public IScanRepository ScanRepository { get; set; }

        public void Scan()
        {
            var website = WebsiteRepository.GetNextScanTargets(1).FirstOrDefault();
            if (website == null)
                throw new NoWebsiteException();

            var scan = ScanService.TriggerScan(website.Hostname);
            scan.Wait();
            ScanRepository.AddWebsiteToScan(scan.Result, website.Id);
            WebsiteRepository.UpdateLastScanDateToNow(website.Id);
        }
    }
}
