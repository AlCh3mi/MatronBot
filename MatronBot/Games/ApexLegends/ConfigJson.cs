using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MatronBot.Games.ApexLegends {
    public struct ConfigJson {
        [JsonProperty("auth")] public string Auth { get; private set; }
        
        public static async Task<ConfigJson> LoadAuthorization() {
            
            var json = string.Empty;

            await using (var fs = File.OpenRead("ApexConfig.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync()
                    .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<ConfigJson>(json);
        }
    }
}