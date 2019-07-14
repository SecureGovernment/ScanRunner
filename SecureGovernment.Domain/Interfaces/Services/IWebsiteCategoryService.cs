using SecureGovernment.Domain.Dtos;
using SecureGovernment.Domain.Models;

namespace SecureGovernment.Domain.Interfaces.Services
{
    public interface IWebsiteCategoryService
    {
        int GetNextId();
        WebsiteCategoryDto GetOrCreateWebsiteCategory(WebsiteEntry websiteEntry);
    }
}
