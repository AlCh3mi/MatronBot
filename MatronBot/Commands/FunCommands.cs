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

        [Command("FlipCoin")]
        public async Task coinFlip(CommandContext ctx) {
            var random = new Random();
            var trueOrFalse = random.Next(2);
            var response = trueOrFalse switch {
                0 => "Heads!",
                1 => "Tails!",
                _ => string.Empty
            };
            
            await ctx.Channel.SendMessageAsync(response);
        }

        [Command("RollDice")]
        public async Task RollDice(CommandContext ctx, int sides) {

            var random = new Random();
            var result = random.Next(sides) + 1;
            await ctx.Channel.SendMessageAsync("You rolled: " + result);
        }
    }
}