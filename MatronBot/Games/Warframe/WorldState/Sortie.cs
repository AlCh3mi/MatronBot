using System.Text;
using System.Text.Json.Serialization;

namespace MatronBot.Games.Warframe.WorldState
{
    public class Sortie
    {
        [JsonInclude] public string id;
        [JsonInclude] public string activation;
        [JsonInclude] public string expiry;
        [JsonInclude] public string rewardPool;
        [JsonInclude] public Variant[] variants;
        [JsonInclude] public string boss;
        [JsonInclude] public string faction;
        [JsonInclude] public bool expired;
        [JsonInclude] public string eta;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{faction}: {boss}");
            sb.AppendLine($"Time Remaining: {eta}");
            sb.AppendLine();
            foreach (var variant in variants)
            {
                sb.AppendLine(variant.ToString());
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public class Variant
        {
            [JsonInclude] public string node;
            [JsonInclude] public string boss;
            [JsonInclude] public string missionType;
            [JsonInclude] public string planet;
            [JsonInclude] public string modifier;
            [JsonInclude] public string modifierDescription;

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine($"{node} : {missionType}");
                sb.AppendLine($"{modifier} : {modifierDescription}");
                return sb.ToString();
            }
        }
    }
}