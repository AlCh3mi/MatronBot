using Newtonsoft.Json;

namespace MatronBot.ApexLegends.Player.Data {
    public class Rank {
        [JsonProperty("rankScore")] public int RankScore { get; private set; }
        [JsonProperty("rankName")] public string RankName{ get; private set; }
        [JsonProperty("rankDiv")] public int RankDiv{ get; private set; }
        [JsonProperty("ladderPosPlatform")] public int LadderPosPlatform{ get; private set; }
        [JsonProperty("rankImg")] public string RankImage{ get; private set; }
        [JsonProperty("rankedSeason")] public string RankedSeason{ get; private set; }
    }
}