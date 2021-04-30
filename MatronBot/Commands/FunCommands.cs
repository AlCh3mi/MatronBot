using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;

namespace MatronBot.Commands {
    public class FunCommands : BaseCommandModule {
        
        [Command("Hello")]
        public async Task Greet(CommandContext ctx) {

            var response = string.Empty;
            var random = new Random();
            response = random.Next(1, 5) switch {
                1 => "Greetings!",
                2 => "Ma se kind!!!!!",
                3 => "Awe!",
                4 => "is it me you're looking for! I can see it in your eyes, I can see it in your smile.. ",
                _ => response
            };

            await ctx.Channel.SendMessageAsync(response).ConfigureAwait(false);
        }
        
        [Command("ping")] 
        [Description("Displays user who issued commands latency to current server.")]
        public async Task Ping(CommandContext ctx) // this command takes no arguments
        {
            // let's trigger a typing indicator to let
            // users know we're working
            await ctx.TriggerTypingAsync();

            // let's make the message a bit more colourful
            var emoji = DiscordEmoji.FromName(ctx.Client, ":ping_pong:");
            
            await ctx.RespondAsync($"{emoji} Pong! Ping: {ctx.Client.Ping}ms");
        }

        [Command("FlipCoin")]
        [Description("Heads or Tails!")]
        [Aliases("CoinFlip")]
        public async Task CoinFlip(CommandContext ctx) {
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

        // [Command("idea")]
        // public async Task Idea(CommandContext ctx) {
        //     var interactivity = ctx.Client.GetInteractivity();
        //
        //     await ctx.Channel.SendMessageAsync("I dont do enough around here? what else would you like me to do?").ConfigureAwait(false);
        //
        //     var message = await interactivity.WaitForMessageAsync(x => 
        //             x.Channel == ctx.Channel && 
        //             x.Author == ctx.User)
        //         .ConfigureAwait(false);
        //     
        //     await ctx.Channel.SendMessageAsync(message.Result.Content).ConfigureAwait(false);
        //     await ctx.Channel.SendMessageAsync("Ill think about it...").ConfigureAwait(false);
        //
        //     botIdeas.Add(message.Result.Content);
        //     
        // }
        
        //todo: turn this into something meaningful
        // [Command("Reaction")]
        // public async Task Reaction(CommandContext ctx) {
        //     var interactivity = ctx.Client.GetInteractivity();
        //
        //     var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);
        //
        //     await ctx.Channel.SendMessageAsync(message.Result.Emoji);
        // }
        
        [Command("Rate")]
        public async Task Rate(CommandContext ctx, params string[] question) {
            
            var desc = string.Empty;

            foreach (var word in question) {
                desc += $"{word} ";
            }
            
            var embed = new DiscordEmbedBuilder {
                Title = "RATE THIS:",
                Description = desc,
                Color = DiscordColor.Gold
            };
            
            var interactivity = ctx.Client.GetInteractivity();
            
            var message = await ctx.Channel.SendMessageAsync(embed: embed).ConfigureAwait(false);
            
            var one = DiscordEmoji.FromName(ctx.Client, ":one:");
            var two = DiscordEmoji.FromName(ctx.Client, ":two:");
            var three = DiscordEmoji.FromName(ctx.Client, ":three:");
            var four = DiscordEmoji.FromName(ctx.Client, ":four:");
            var five = DiscordEmoji.FromName(ctx.Client, ":five:");
            
            await message.CreateReactionAsync(one).ConfigureAwait(false);
            await message.CreateReactionAsync(two).ConfigureAwait(false);
            await message.CreateReactionAsync(three).ConfigureAwait(false);
            await message.CreateReactionAsync(four).ConfigureAwait(false);
            await message.CreateReactionAsync(five).ConfigureAwait(false);
        }
        
        [Command("Poll")]
        public async Task Poll(CommandContext ctx, params string[] question) {

            var desc = string.Empty;

            foreach (var word in question) {
                desc += $"{word} ";
            }
            
            var joinEmbed = new DiscordEmbedBuilder {
                Title = "POLL:",
                Description = desc,
                Color = DiscordColor.Purple
            };

            var joinMessage = await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

            var thumbsUpEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
            var thumbsDownEmoji = DiscordEmoji.FromName(ctx.Client, ":-1:");

            await joinMessage.CreateReactionAsync(thumbsUpEmoji).ConfigureAwait(false);
            await joinMessage.CreateReactionAsync(thumbsDownEmoji).ConfigureAwait(false);

            var interactivity = ctx.Client.GetInteractivity();

            var reactionResult = await interactivity.WaitForReactionAsync(x =>
                x.Message == joinMessage &&
                x.User == ctx.User &&
                (x.Emoji == thumbsUpEmoji || x.Emoji == thumbsDownEmoji))
                .ConfigureAwait(false);
        }
    }
}