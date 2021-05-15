using System.Collections.Generic;
using MatronBot.ApexLegends.Map.Data;
using Newtonsoft.Json;

namespace MatronBot.ApexLegends.Map {
    public class MapInfo {
        public readonly Dictionary<string, GameMode> Modes;
        
        public MapInfo(string serverResponse) {
            Modes = JsonConvert.DeserializeObject<Dictionary<string, GameMode>>(serverResponse);
        }

        public override string ToString() {
            var response = string.Empty;
            foreach (var mode in Modes) {
                response += $"{mode.ToString()}\n";
            }
            return response;
        }
    }
}