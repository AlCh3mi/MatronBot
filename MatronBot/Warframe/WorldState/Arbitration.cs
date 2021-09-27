using System.Text;
using System.Text.Json.Serialization;

namespace WarframeAPI.WorldState
{
    public class Arbitration
    {
        [JsonInclude] public string activation;
        [JsonInclude] public string expiry;
        [JsonInclude] public string node;
        [JsonInclude] public string enemy;
        [JsonInclude] public string type;
        [JsonInclude] public bool archwing;
        [JsonInclude] public bool sharkwing;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Arbitration: {type} vs {enemy} on {node}");
            if (archwing)
                sb.AppendLine("This is an Archwing mission.");
            if (sharkwing)
                sb.AppendLine("This is a SHARKWING mission");
            sb.AppendLine($"expires at {expiry}");
            return sb.ToString();
        }
    }
}