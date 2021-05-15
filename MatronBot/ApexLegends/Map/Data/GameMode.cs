namespace MatronBot.ApexLegends.Map.Data {
    public class GameMode {
        public Map Current;
        public Map Next;

        public const string BattleRoyale = "battle_royale";
        public const string Arenas = "arenas";
        public const string Ranked = "ranked";

        public override string ToString() => $"Current: {Current} Next: {Next}";
    }
}