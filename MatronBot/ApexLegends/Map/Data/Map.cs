using System;
using Newtonsoft.Json;

namespace MatronBot.ApexLegends.Map.Data {
    public class Map {
        [JsonProperty("readableDate_start")] public DateTime ReadableDateStart { get; private set; }
        [JsonProperty("readableDate_end")] public DateTime ReadableDateEnd { get; private set; }
        [JsonProperty("map")] public string Name { get; private set; }
        [JsonProperty("DurationInMinutes")] public int DurationInMinutes { get; private set; }
        [JsonProperty("remainingMins")] public int RemainingMins { get; private set; }

        public override string ToString() => Name;
    }
}