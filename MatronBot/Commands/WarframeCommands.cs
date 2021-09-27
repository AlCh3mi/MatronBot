using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using WarframeAPI;

namespace MatronBot.Commands
{
    public class WarframeCommands : BaseCommandModule
    {
        [Command("Sortie")]
        public async Task Sortie(CommandContext ctx)
        {
            var warframeStatApi = new WarframeStatApi(Platform.Pc);
            await ctx.Channel.SendMessageAsync(warframeStatApi.Sortie().ToString());
        }
        
        [Command("Arbitration")]
        public async Task Arbitration(CommandContext ctx)
        {
            var warframeStatApi = new WarframeStatApi(Platform.Pc);
            var arbitration = warframeStatApi.Arbitration();
            
            var embedBuilder = new DiscordEmbedBuilder()
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
            await ctx.Channel.SendMessageAsync(sb.ToString());
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
                await ctx.Channel.SendMessageAsync(embed);
            }
            else
            {
                var embed = new DiscordEmbedBuilder
                {
                    Title = $"{baro.character}",
                    Description = $"will be here in  {baro.startString}",
                    Color = DiscordColor.Purple
                };
                await ctx.Channel.SendMessageAsync(embed);
            }
            
        }
    }
}