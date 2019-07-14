using SecureGovernment.Domain.Dtos;
using SecureGovernment.Domain.Models;

namespace SecureGovernment.Domain.Tests
{
    public sealed class DataCreationHelper
    {
        private const Enums.GovernmentType GOVERNMENT_TYPE = Enums.GovernmentType.FEDERAL_EXECUTIVE;
        private const string AGENCY_NAME = "General Services Administration";
        private const string ORGANIZATION_NAME = "Office of Citizen Services and Communications";
        private const string CITY = "Washington";
        private const string STATE = "DC";

        public static WebsiteEntry CreateStandardWebsiteEntry() => new WebsiteEntry()
        {
            Hostname = "usa.gov",
            GovernmentType = GOVERNMENT_TYPE,
            Agency = AGENCY_NAME,
            Organization = ORGANIZATION_NAME,
            ContactEmail = "(blank)",
            City = CITY,
            State = STATE
        };

        public static WebsiteCategoryDto CreateStandardWebsiteCategoryDto() => new WebsiteCategoryDto()
        {
            Id = 21,
            GovernmentType = (int)GOVERNMENT_TYPE,
            Agency = AGENCY_NAME,
            Organization = ORGANIZATION_NAME,
            City = CITY,
            State = STATE
        };


    }
}
