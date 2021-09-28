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
    }
}