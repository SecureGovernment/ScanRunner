using SecureGovernment.Domain.Enums;

namespace SecureGovernment.Domain.Models
{
    public class WebsiteEntry
    {
        public string Hostname { get; set; }
        public GovernmentType GovernmentType { get; set; }
        public string Agency { get; set; }
        public string Organization { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ContactEmail { get; set; }
    }
}
