using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MatronBot.ApexLegends {
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

        public static string SubSection(string header, string value, string separator = "},") {
            if (!(value.Contains(header) && value.Contains(separator)) || string.IsNullOrEmpty(value)) return null;

            var answer = string.Empty;
            var index = value.IndexOf(header, StringComparison.Ordinal);
            var separatorIndex = value.IndexOf(separator, index, StringComparison.Ordinal);

            for (var i = index + header.Length; i < separatorIndex; i++) {
                answer += value[i];
            }

            return answer;
        }
        
        public static string FindInResponse(string header, string toSearch) {
            if (!toSearch.Contains(header)) return null;
            var indexOf = toSearch.IndexOf(header, StringComparison.Ordinal);
            var response = string.Empty;
            for (var i = indexOf + header.Length + 2; i < toSearch.Length; i++) {
                if (toSearch[i] == ',') break;
                response += toSearch[i];
            }

            response = response.Trim();
            return response.Trim('\"');
        }

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