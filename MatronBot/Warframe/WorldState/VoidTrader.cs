using System.Text;
using System.Text.Json.Serialization;

namespace MatronBot.Warframe.WorldState
{
    public class VoidTrader
    {
        [JsonInclude] public string id;
        [JsonInclude] public string activation;
        [JsonInclude] public string expiry;
        [JsonInclude] public string character;
        [JsonInclude] public string location;
        [JsonInclude] public VoidTraderItem[] inventory;
        [JsonInclude] public string psId;
        [JsonInclude] public bool active;
        [JsonInclude] public string startString;
        [JsonInclude] public string endString;

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (active)
            {
                sb.AppendLine($"{character} is at {location}");

                foreach (var item in inventory)
                {
                    sb.AppendLine(item.ToString());
                }

                sb.AppendLine($"He will be leaving in {endString}");
            }
            else
            {
                sb.AppendLine($"{character} will be here in {startString}");
            }

            return sb.ToString();

        }

        public class VoidTraderItem
        {
            [JsonInclude] public string item;
            [JsonInclude] public int ducats;
            [JsonInclude] public int credits;

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Item: {item}");
                sb.AppendLine($"Price: {ducats} ducats, {credits} credits");
                return sb.ToString();
            }
        }
    }
}