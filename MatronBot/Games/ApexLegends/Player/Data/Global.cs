using Newtonsoft.Json;

namespace MatronBot.Games.ApexLegends.Player.Data {
    public class Global {
        [JsonProperty("name")] public string Name { get; private set; }
        [JsonProperty("uid")] public long Uid { get; private set; }
        [JsonProperty("avatar")] public string Avatar { get; private set; }
        [JsonProperty("platform")] public string Platform { get; private set; }
        [JsonProperty("level")] public int Level { get; private set; }
        [JsonProperty("toNextLevelPercent")] public int ToNextLevelPercent { get; private set; }
        [JsonProperty("internalUpdateCount")] public int InternalUpdateCount { get; private set; }
        [JsonProperty("bans")] public Bans Bans { get; private set; }
        [JsonProperty("rank")] public Rank Rank { get; private set; }
        [JsonProperty("battlepass")] public BattlePass BattlePass { get; private set; }

        public override string ToString() {
            return $"{Name}: Level: {Level}.{ToNextLevelPercent} - {Rank.RankName} {Rank.RankDiv} " +
                   $"\n{(Bans.IsActive ? $"BANNED : {Bans.LastBanReason} for {Bans.RemainingSeconds}" : "Not currently Banned")}";
        }
    }
}