using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace MatronBot.Commands {
    public class GamingCommands : BaseCommandModule {
        
        int Deaths;    
        
        [Command("Death")] 
        public async Task Death(CommandContext ctx) {
            Deaths++;
            await DeathCount(ctx);
        }
        [Command("DeathCount")] 
        public async Task DeathCount(CommandContext ctx){
            await ctx.Channel.SendMessageAsync(Deaths.ToString()).ConfigureAwait(false);
        }
        
        [Command("ResetDeathCounter")] 
        public async Task ResetDeathCounter(CommandContext ctx) {
            Deaths = 0;
            await DeathCount(ctx);
        }
    }
}