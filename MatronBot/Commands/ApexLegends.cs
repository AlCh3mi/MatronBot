using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;

namespace MatronBot.Commands {
    public class ApexLegends : BaseCommandModule {
        
        Dictionary<string, int> apexWins = new Dictionary<string, int>();
        readonly string savePath = Path.Combine(Directory.GetCurrentDirectory(), "ApexWins.orphan");
        
        [Command("ApexWin")]
        public async Task ApexWin(CommandContext ctx) {

            if (apexWins.ContainsKey(ctx.User.Username)) {
                
                apexWins[ctx.User.Username] += 1;
            }
            else {
                apexWins.Add(ctx.User.Username, 1);
            }

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
            await Load(savePath);
            await ctx.Channel.SendMessageAsync("Apex Wins Loaded");
        }

        [Command("ApexSave")]
        public async Task ApexSave(CommandContext ctx) {
            await Save(savePath);
            await ctx.Channel.SendMessageAsync($"Apex Wins Saved to {savePath}");
        }

        [Command("ApexMap")]
        public async Task ApexMap(CommandContext ctx) {
            var response = await ApiRequest("https://api.mozambiquehe.re/maprotation?auth=X8MmHiCTDGB3tCgZe0iv");
            await ctx.Channel.SendMessageAsync(response);
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
        
        async Task<string> ApiRequest(string url)
        {
            ServicePointManager.ServerCertificateValidationCallback = 
                (a, b, c, d) => true;            

            var req = (HttpWebRequest)WebRequest.Create(url);
            var resp = (HttpWebResponse) await req.GetResponseAsync();
            using var sr = new StreamReader(resp.GetResponseStream());
            var results = await sr.ReadToEndAsync();
            return results;
        }
        
        //Hi olpan
    }
}