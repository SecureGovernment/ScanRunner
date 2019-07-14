using SecureGovernment.Domain.Dtos;
using SecureGovernment.Domain.Interfaces.Repositories;
using SecureGovernment.Domain.Interfaces.Services;
using SecureGovernment.Domain.Models;

namespace SecureGovernment.Domain.Services
{
    public class WebsiteCategoryService : IWebsiteCategoryService
    {
        public IWebsiteCategoryRepository WebsiteCategoryRepository { get; set; }

        public int GetNextId() => WebsiteCategoryRepository.GetLargestId() + 1;

        public WebsiteCategoryDto GetOrCreateWebsiteCategory(WebsiteEntry websiteEntry)
        {
            var websiteCategory = WebsiteCategoryRepository.GetWebsiteCategory(websiteEntry.GovernmentType, websiteEntry.Agency, websiteEntry.Organization, websiteEntry.City, websiteEntry.State);
            if (websiteCategory == null)
            {
                websiteCategory = new WebsiteCategoryDto()
                {
                    Id = GetNextId(),
                    GovernmentType = (int)websiteEntry.GovernmentType,
                    Agency = websiteEntry.Agency,
                    Organization = websiteEntry.Organization,
                    State = websiteEntry.State,
                    City = websiteEntry.City
                };
                WebsiteCategoryRepository.Add(websiteCategory);
            }

            return websiteCategory;
        }
    }
}
