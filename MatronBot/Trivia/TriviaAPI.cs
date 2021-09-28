using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MatronBot.Trivia
{
    public class TriviaApi
    {
        public string Request(string apiRequest)
            => Task.Run(() => RequestAsync(apiRequest)).Result.Replace("&quot;", "\"").Replace("&#039;", "\'").Replace("&amp;", "&");
        
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