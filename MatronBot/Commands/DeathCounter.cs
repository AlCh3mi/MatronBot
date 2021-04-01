using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace MatronBot.Commands {
    public class DeathCounter : BaseCommandModule {
        
        Dictionary<string, int> necronomicon = new Dictionary<string, int>();
        
        [Command("Death")] 
        public async Task Death(CommandContext ctx) {
            if(necronomicon.ContainsKey(ctx.User.Username))
                necronomicon[ctx.User.Username]++;
            else {
                necronomicon.Add(ctx.User.Username, 1);
            }
            await DeathCount(ctx);
        }
        [Command("DeathCount")] 
        public async Task DeathCount(CommandContext ctx){
            await ctx.Channel.SendMessageAsync($"{ctx.User.Username} Deaths: {necronomicon[ctx.User.Username]}").ConfigureAwait(false);
        }
        
        [Command("DeathReset")] 
        public async Task DeathReset(CommandContext ctx) {
            necronomicon[ctx.User.Username] = 0;
            await DeathCount(ctx);
        }
    }
}