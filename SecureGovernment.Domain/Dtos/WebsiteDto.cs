using SecureGovernment.Domain.Enums;
using System;

namespace SecureGovernment.Domain.Dtos
{
    public class WebsiteDto
    {
        public int Id { get; set; }
        public string Hostname { get; set; }
        public DateTime LastScan { get; set; }
        public StatusType Status { get; set; }
        public int? CategoryId { get; set; }
    }
}
