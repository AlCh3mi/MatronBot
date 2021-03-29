using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace MatronBot.Commands {
    public class FunCommands : BaseCommandModule {
        
        [Command("Hello")]
        public async Task Sup(CommandContext ctx) {
            var response = string.Empty;
            Random random = new Random();
            response = random.Next(1, 5) switch {
                1 => "Greetings!",
                2 => "Ma se kind!!!!!",
                3 => "Awe!",
                4 => "is it me you're looking for! I can see it in your eyes, I can see it in your smile.. ",
                _ => response
            };
            await ctx.Channel.SendMessageAsync(response).ConfigureAwait(false);
        }
        
        [Command("Tubs")]
        public async Task Tubs(CommandContext ctx) {
            await ctx.Member.SendMessageAsync("DA BOOOOMB!!!!!").ConfigureAwait(false);
        }
    }
}