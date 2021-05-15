using Newtonsoft.Json;

namespace MatronBot.ApexLegends.Player.Data {
    public class Stats {
        [JsonProperty("global")]public Global Global { get; private set; }
        [JsonProperty("realtime")] public RealTime RealTime { get; private set; }

        public override string ToString() {
            return $"{Global}\n" +
                   $"{RealTime}";
        }
    }
}