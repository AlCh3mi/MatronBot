using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MatronBot.Games.ApexLegends {
    public class ApiInteraction {
        //Player Info Request
        //https://api.mozambiquehe.re/bridge?platform=PC&player=DeadwoodZa&auth={config.Auth}

        //Current Map Info
        //https://api.mozambiquehe.re/maprotation?auth={config.Auth}

        public ConfigJson Config { get; }

        public ApiInteraction() {
            Config = ConfigJson.LoadAuthorization().Result;
        }
        
        public string Request(string apiRequest)
            => Task.Run(() => RequestAsync(apiRequest)).Result;
        
        static async Task<string> RequestAsync(string url) {
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