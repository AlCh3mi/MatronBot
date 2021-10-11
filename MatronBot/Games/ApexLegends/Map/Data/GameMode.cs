namespace MatronBot.Games.ApexLegends.Map.Data {
    public class GameMode {
        public Map Current;
        public Map Next;

        public static readonly string BattleRoyale = "battle_royale";
        public static readonly string Arenas = "arenas";
        public static readonly string Ranked = "ranked";

        public override string ToString() => $"Current: {Current} Next: {Next}";
    }
}