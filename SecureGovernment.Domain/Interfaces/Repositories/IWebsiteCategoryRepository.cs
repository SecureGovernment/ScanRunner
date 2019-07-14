using SecureGovernment.Domain.Dtos;
using SecureGovernment.Domain.Enums;

namespace SecureGovernment.Domain.Interfaces.Repositories
{
    public interface IWebsiteCategoryRepository
    {
        WebsiteCategoryDto GetWebsiteCategory(GovernmentType domainType, string agency, string organization, string city, string state);
        void Add(WebsiteCategoryDto websiteCategoryDto);
        int GetLargestId();
    }
}
