namespace SecureGovernment.Domain.Interfaces.Infastructure
{
    public interface ISettings
    {
        string Version { get; set; }
        string ConnectionString { get; set; }
        string LogDirectory { get; set; }
        string TlsObservatoryApiUrl { get; set; }
    }
}
