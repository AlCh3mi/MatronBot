using System.Collections.Generic;
using Newtonsoft.Json;

namespace MatronBot.ApexLegends.Player.Data {
    public class BattlePass {
        [JsonProperty("level")] public int Level { get; private set; }    
        [JsonProperty("history")] public Dictionary<string, int> History { get; private set; }
    }
}