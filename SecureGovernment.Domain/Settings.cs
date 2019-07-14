using SecureGovernment.Domain.Interfaces.Infastructure;

namespace SecureGovernment.Domain
{
    public class Settings : ISettings
    {
        public string Version { get; set; }
        public string ConnectionString { get; set; }
        public string LogDirectory { get; set; }
        public string TlsObservatoryApiUrl { get; set; }
    }
}
