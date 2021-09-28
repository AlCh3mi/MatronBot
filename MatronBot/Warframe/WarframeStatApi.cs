using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using MatronBot.Warframe.WorldState;

namespace MatronBot.Warframe
{
    public class WarframeStatApi
    {
        //https://api.warframestat.us/{platform}/{request}

        private const string Uri = "https://api.warframestat.us/";
        private readonly string _platform;

        public WarframeStatApi(Platform platform)
        {
            _platform = platform switch
            {
                Platform.Pc => "pc/",
                Platform.PlayStation => "ps4/",
                Platform.Xbox => "xb1/",
                Platform.Switch => "swi/",
                _ => "pc/"
            };
        }

        public Sortie Sortie() => JsonSerializer.Deserialize<Sortie>(Request(GetRequests.Sortie));

        public Fissure[] Fissures() => JsonSerializer.Deserialize<Fissure[]>(Request(GetRequests.Fissures));
        
        public VoidTrader VoidTrader() => JsonSerializer.Deserialize<VoidTrader>(Request(GetRequests.Baro));

        public Arbitration Arbitration() => JsonSerializer.Deserialize<Arbitration>(Request(GetRequests.Arbitration));
        
        private string Request(string apiRequest)
            => Task.Run(() => RequestAsync($"{Uri}{_platform}{apiRequest}")).Result;
        
        private static async Task<string> RequestAsync(string url) {
            ServicePointManager.ServerCertificateValidationCallback =
                (a, b, c, d) => true;
            var req = (HttpWebRequest) WebRequest.Create(url);
            var resp = (HttpWebResponse) await req.GetResponseAsync();
            using var sr = new StreamReader(resp.GetResponseStream());
            var results = await sr.ReadToEndAsync();
            return results;
        }
    }
}