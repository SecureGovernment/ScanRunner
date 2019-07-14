namespace SecureGovernment.Domain.Interfaces.Repositories
{
    public interface IScanRepository
    {
        void AddWebsiteToScan(int scanId, int websiteId);
    }
}
