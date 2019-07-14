using Newtonsoft.Json;
using SecureGovernment.Domain.Interfaces.Infastructure;
using SecureGovernment.Domain.Interfaces.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecureGovernment.Domain.Services
{
    public class ScanService : IScanService
    {
        public ISettings Settings { get; set; }

        private static readonly dynamic SCAN_DEFINITION = new { scan_id = 0 };
        private static HttpClient _HttpClient { get; set; }
        public static HttpClient HttpClient {
            get {
                if (_HttpClient == null) _HttpClient = new HttpClient();
                return _HttpClient;
            }
            set => _HttpClient = value;
        }

        public async Task<int> TriggerScan(string hostname)
        {
            var response = await HttpClient.PostAsync(string.Format(Settings.TlsObservatoryApiUrl, hostname), null);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var scanId = JsonConvert.DeserializeAnonymousType(responseJson, SCAN_DEFINITION);
                return scanId.scan_id;
            }
            
            throw new HttpRequestException();
        }
    }
}
