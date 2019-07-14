using SecureGovernment.Domain.Enums;

namespace SecureGovernment.Domain.Dtos
{
    public class WebsiteCategoryDto
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Organization { get; set; }
        public string Agency { get; set; }
        public int GovernmentType { get; set; }
    }
}
