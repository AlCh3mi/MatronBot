using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MatronBot.Games.Warframe;

namespace MatronBot.Commands
{
    public class WarframeCommands : BaseCommandModule
    {
        [Command("Sortie")]
        public async Task Sortie(CommandContext ctx)
        {
            var warframeStatApi = new WarframeStatApi(Platform.Pc);
            var sortie = warframeStatApi.Sortie();
            
            await ctx.Channel.SendMessageAsync($"{sortie.faction} Sortie : {sortie.eta}");

            var embedSortie1 = new DiscordEmbedBuilder
            {
                Title = $"Sortie 1: {sortie.variants[0].missionType} : {sortie.variants[0].node}",
                Description = $"{sortie.variants[0].modifier}",
                Color = DiscordColor.White
            };
            
            var embedSortie2 = new DiscordEmbedBuilder
            {
                Title = $"Sortie 2: {sortie.variants[1].missionType} : {sortie.variants[1].node}",
                Description = $"{sortie.variants[1].modifier}",
                Color = DiscordColor.CornflowerBlue
            };
            
            var embedSortie3 = new DiscordEmbedBuilder
            {
                Title = $"Sortie 3: {sortie.variants[2].missionType} : {sortie.variants[2].node}",
                Description = $"{sortie.variants[2].modifier}",
                Color = DiscordColor.MidnightBlue
            };

            await ctx.Channel.SendMessageAsync(embedSortie1);
            await ctx.Channel.SendMessageAsync(embedSortie2);
            await ctx.Channel.SendMessageAsync(embedSortie3);

        }
        
        [Command("Arbitration")]
        public async Task Arbitration(CommandContext ctx)
        {
            var warframeStatApi = new WarframeStatApi(Platform.Pc);
            var arbitration = warframeStatApi.Arbitration();
            
            var embedBuilder = new DiscordEmbedBuilder
            {
                Title = "Arbitration",
                Description = $"{arbitration.type} on {arbitration.node}",
                Color = DiscordColor.Rose
            };
            await ctx.Channel.SendMessageAsync(embedBuilder);
        }
        
        [Command("Fissures")]
        public async Task Fissures(CommandContext ctx)
        {
            var warframeStatApi = new WarframeStatApi(Platform.Pc);
            var sb = new StringBuilder();
            foreach (var fissure in warframeStatApi.Fissures())
            {
                sb.AppendLine(fissure.ToString());
            }

            var embed = new DiscordEmbedBuilder
            {
                Title = "Fissures",
                Description = sb.ToString(),
                Color = DiscordColor.IndianRed
            };
            await ctx.Channel.SendMessageAsync(embed);
        }
        
        [Command("Baro")]
        public async Task VoidTrader(CommandContext ctx)
        {
            var warframeStatApi = new WarframeStatApi(Platform.Pc);
            var baro = warframeStatApi.VoidTrader();
            
            if (baro.active)
            {
                var embed = new DiscordEmbedBuilder
                {
                    Title = $"{baro.character}",
                    Description = $"is currently on {baro.location}",
                    Color = DiscordColor.Purple
                };

                var items = string.Empty;
                
                foreach (var item in baro.inventory)
                {
                    items += $"\n{item.item} Credits:{item.credits} Ducats:{item.ducats}";
                }

                embed.Description += items;
                await ctx.Channel.SendMessageAsync(embed);
            }
            else
            {
                var embed = new DiscordEmbedBuilder
                {
                    Title = $"{baro.character}",
                    Description = $"will be here in {baro.startString}",
                    Color = DiscordColor.Purple
                };
                await ctx.Channel.SendMessageAsync(embed);
            }
        }
    }
}