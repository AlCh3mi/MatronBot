﻿using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;

namespace MatronBot.Commands {
    public class ApexLegends : BaseCommandModule {
        Dictionary<string, int> apexWins = new Dictionary<string, int>();
        string savePath = Path.Combine(Directory.GetCurrentDirectory(), "ApexWins.orphan");
        ApexQuotes apexQuotes;
        
        [Command("ApexWin")]
        public async Task ApexWin(CommandContext ctx) {

            if (apexWins.ContainsKey(ctx.User.Username)) {
                apexWins[ctx.User.Username] += 1;
            }
            else {
                apexWins.Add(ctx.User.Username, 1);
            }

            //await ctx.Channel.SendMessageAsync(apexQuotes.GetRandomQuote()).ConfigureAwait(false);
            await ctx.Member.SendMessageAsync($"Wins : {apexWins[ctx.User.Username]}").ConfigureAwait(false);
            await Save(savePath);
        }

        [Command("ApexLeaderboard")]
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
            apexQuotes = new ApexQuotes();
            
            await Load(savePath);
            await ctx.Channel.SendMessageAsync("Apex Wins Loaded");
        }

        [Command("ApexSave")]
        public async Task ApexSave(CommandContext ctx) {
            await Save(savePath);
            await ctx.Channel.SendMessageAsync($"Apex Wins Saved to {savePath}");
        }

        [Command("ApexAddQuote")]
        public async Task ApexAddQuote(CommandContext ctx, string quote) {
            apexQuotes.AddQuote(quote);
            await ctx.Channel.SendMessageAsync($"Quote added: {quote}").ConfigureAwait(false);
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