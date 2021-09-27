using System.Text;
using System.Text.Json.Serialization;

namespace WarframeAPI.WorldState
{
    public class Fissure
    {
        [JsonInclude] public string node;
        [JsonInclude] public bool expired;
        [JsonInclude] public string eta;
        [JsonInclude] public string missionType;
        [JsonInclude] public string tier;
        [JsonInclude] public int tierNum;
        [JsonInclude] public string enemy;
        [JsonInclude] public string id;
        [JsonInclude] public string expiry;
        [JsonInclude] public string activation;
        [JsonInclude] public bool isStorm;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{tier} - {missionType}");
            sb.AppendLine($"vs {enemy} on {node}");
            sb.AppendLine($"Time remaining: {eta}");
            return sb.ToString();
        }
    }
}