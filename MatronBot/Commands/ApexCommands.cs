using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MatronBot.ApexLegends;
using MatronBot.ApexLegends.Map;
using MatronBot.ApexLegends.Map.Data;
using MatronBot.ApexLegends.Player;
using Newtonsoft.Json;

namespace MatronBot.Commands {
    public class ApexCommands : BaseCommandModule {
        
        Dictionary<string, int> apexWins = new Dictionary<string, int>();
        readonly string savePath = Path.Combine(Directory.GetCurrentDirectory(), "ApexWins.orphan");
        
        [Command("ApexWin")]
        [Description("Register your win on the current monthly leaderboard. Check apexLeaderboard command to see rankings.")]
        public async Task ApexWin(CommandContext ctx) {

            if (apexWins.ContainsKey(ctx.User.Username)) {
                
                apexWins[ctx.User.Username] += 1;
            }
            else {
                apexWins.Add(ctx.User.Username, 1);
            }

            await ctx.Member.SendMessageAsync($"Wins : {apexWins[ctx.User.Username]}").ConfigureAwait(false);
            await Save(savePath);
        }

        [Command("ApexLeaderboard")]
        [Description("Displays all current players entered to this months leaderboard and their registered wins. Post your proof in #apexwin-screenshots")]
        public async Task ApexLeaderboard(CommandContext ctx) {
            await ctx.Channel.SendMessageAsync("Apex wins: ");

            StringBuilder sb = new StringBuilder();
            
            foreach (var entry in apexWins) {
                sb.Append($"{entry.Value} : {entry.Key} \n");
            }
            await ctx.Channel.SendMessageAsync(sb.ToString()).ConfigureAwait(false);
        }

        [Command("ApexReset")]
        [RequireRoles(RoleCheckMode.All, "Orphan", "Apex")]
        public async Task ApexLeaderBoardReset(CommandContext ctx) {
            apexWins = new Dictionary<string, int>();
            await ctx.Channel.SendMessageAsync("Apex Leaderboard Reset").ConfigureAwait(false);
            await Save(savePath);
        }

        [Command("ApexLoad")]
        public async Task ApexLoad(CommandContext ctx) {
            await Load(savePath);
            await ctx.Channel.SendMessageAsync("Apex Wins Loaded");
        }

        [Command("ApexSave")]
        public async Task ApexSave(CommandContext ctx) {
            await Save(savePath);
            await ctx.Channel.SendMessageAsync($"Apex Wins Saved to {savePath}");
        }

        [Command("ApexMap")]
        [Description("Provides Current Apex map rotation info. Optionally \"pubs\" \"arenas\" and \"ranked\" can be added for more detailed info.")]
        public async Task ApexMap(CommandContext ctx, string mode = "all") {
            
            var api = new ApiInteraction();
            var response = api.Request($"https://api.mozambiquehe.re/maprotation?version=2&auth={api.Config.Auth}");
            var mapInfo = new MapInfo(response);
            switch (mode) {
                
                case "br":    
                case "pubs":
                case "trios":
                case "battle_royale":
                case "battleroyale":
                    var pubs = $"Current : {mapInfo.Modes[GameMode.BattleRoyale].Current} for {mapInfo.Modes[GameMode.BattleRoyale].Current.RemainingMins} more mins.\n" +
                                    $"Next     : {mapInfo.Modes[GameMode.BattleRoyale].Next}";
                    await ctx.Channel.SendMessageAsync(pubs);
                    break;
                case "a":
                case "arena":
                case "arenas":
                    var arenas = $"Current : {mapInfo.Modes[GameMode.Arenas].Current} for {mapInfo.Modes[GameMode.Arenas].Current.RemainingMins} more mins.\n" +
                                 $"Next     : {mapInfo.Modes[GameMode.Arenas].Next}";
                    await ctx.Channel.SendMessageAsync(arenas);
                    break;
                case "r":
                case "ranked":
                    await ctx.Channel.SendMessageAsync($"Current : {mapInfo.Modes[GameMode.Ranked].Current}\n" +
                                                       $"Next     : {mapInfo.Modes[GameMode.Ranked].Next}");
                    break;
                
                default:
                    await ctx.Channel.SendMessageAsync(mapInfo.ToString());
                    break;
            }
        }

        [Command("ApexPlayer")]
        [Description("Provides info about a players stats. Such as Account Level, Rank and Online Status")]
        public async Task ApexPlayer(CommandContext ctx, string playerName) {
            var api = new ApiInteraction();
            var response = api.Request($"https://api.mozambiquehe.re/bridge?platform=PC&player={playerName}&auth={api.Config.Auth}");
            var playerInfo = new Info(response);

            await ctx.Channel.SendMessageAsync(playerInfo.Stats.Global.ToString());
            await ctx.Channel.SendMessageAsync(playerInfo.Stats.RealTime.ToString());
        }

        async Task Save(string path) {
            await using var sw = new StreamWriter(path);
            await sw.WriteAsync(JsonConvert.SerializeObject(apexWins));
        }

        async Task Load(string path) {
            using var sr = new StreamReader(path);
            var tmp = await sr.ReadToEndAsync().ConfigureAwait(false);
            apexWins = JsonConvert.DeserializeObject<Dictionary<string, int>>(tmp);
        }
    }
}