using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MatronBot.Games.ApexLegends;
using MatronBot.Games.ApexLegends.Map;
using MatronBot.Games.ApexLegends.Map.Data;
using MatronBot.Games.ApexLegends.Player;
using MatronBot.Games.ApexLegends.Player.Data;

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

            var discordEmbedBuilder = new DiscordEmbedBuilder
            {
                Title = $"{playerInfo.Stats.Global.Name}",
                Description = $"{playerInfo.Stats.Global.Rank.RankName} {playerInfo.Stats.Global.Rank.RankDiv} - ({playerInfo.Stats.Global.Rank.RankScore})",
                Color = playerInfo.Stats.RealTime.IsOnline == 1? DiscordColor.Green : DiscordColor.Red,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    IconUrl = playerInfo.Stats.Global.Avatar,
                    Text = $"UID: {playerInfo.Stats.Global.Uid}"
                },
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                {
                    Height = 100,
                    Width = 100,
                    Url = playerInfo.Stats.Global.Rank.RankImage
                }
            };

            discordEmbedBuilder.AddField("Legend", playerInfo.Stats.RealTime.SelectedLegend);
            
            if (playerInfo.Stats.RealTime.IsOnline != 0)
            {
                discordEmbedBuilder.AddField("Party Full", playerInfo.Stats.RealTime.PartyFull == 1 ? "Yes" : "No", true);
                discordEmbedBuilder.AddField("In Game", playerInfo.Stats.RealTime.IsInGame == 1 ? "Yes" : "No", true);
                discordEmbedBuilder.AddField("Can Join", playerInfo.Stats.RealTime.CanJoin == 1 ? "Yes" : "No", true);
            }

            await ctx.Channel.SendMessageAsync(discordEmbedBuilder);
        }
    }
}