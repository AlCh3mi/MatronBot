using Newtonsoft.Json;

namespace MatronBot.Games.ApexLegends.Player.Data {
    public class RealTime {
        [JsonProperty("lobbyState")] public string LobbyState { get; private set; }
        [JsonProperty("isOnline")] public int IsOnline { get; private set; }
        [JsonProperty("isInGame")] public int IsInGame { get; private set; }
        [JsonProperty("canJoin")] public int CanJoin { get; private set; }
        [JsonProperty("partyFull")] public int PartyFull { get; private set; }
        [JsonProperty("selectedLegend")] public string SelectedLegend { get; private set; }

        public override string ToString() {
            return $"Is Online:         {(IsOnline == 1 ? "Online" : "Offline")}\n" +
                   $"Is In Game:        {(IsInGame == 1 ? "Yes" : "No")}\n" +
                   $"Can Join:          {(CanJoin == 1 ? "Yes" : "No")} \n" +
                   $"Party Full:        {(PartyFull == 1 ? "Yes" : "No")}\n" +
                   $"Selected Legend:   {SelectedLegend}";
        }
    }
}