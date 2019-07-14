using ScanRunner.Data.Entities;
using SecureGovernment.Data.Infastructure;
using SecureGovernment.Domain.Enums;
using SecureGovernment.Domain.Interfaces.Infastructure;
using SecureGovernment.Domain.Interfaces.Repositories;
using System;
using System.Linq;

namespace SecureGovernment.Data.Repositories
{
    public class ScanRepository : DatabaseRepository<Scans>, IScanRepository
    {
        public ScanRepository(IDatabaseFactory factory) : base(factory){}
        public IUnitOfWorkFactory UnitOfWorkFactory { get; set; }

        private Scans GetScan(int scanId)
        {
            return (from scan in this.DbSet
                    where scan.Id == scanId
                    select scan).SingleOrDefault();
        }

        public void AddWebsiteToScan(int scanId, int websiteId)
        {
            var scan = GetScan(scanId);
            if (scan == null) throw new ArgumentNullException("scan", $"Could not link website #{websiteId} to scan. The database does not have the scan with an id of {scanId}.");

            scan.WebsiteId = websiteId;

            using (var unitOfWork = UnitOfWorkFactory.GetUnitOfWork())
            {
                unitOfWork.Queue(new EntityCommand<Scans>(CommandType.UPDATE, scan));
            }    
        }
    }
}
