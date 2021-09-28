using System.Text.Json.Serialization;

namespace MatronBot.Jokes
{
    public class ChuckNorris
    {
        [JsonInclude] public string icon_url;
        [JsonInclude] public string id;
        [JsonInclude] public string url;
        [JsonInclude] public string value;
    }
}