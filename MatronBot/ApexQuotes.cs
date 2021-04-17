using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MatronBot {
    public class ApexQuotes {

        public List<string> quotes;

        readonly string quotesPath = Path.Combine(Directory.GetCurrentDirectory(), "ApexQuotes.orphan");

        public ApexQuotes() {
            quotes = new List<string>();
            Task.Run(() => ReadQuotesFile(quotesPath));
        }
        
        public string GetRandomQuote() {
            var random = new Random(quotes.Count);
            return quotes[random.Next()];
        }

        public void AddQuote(string quote) {
            quotes.Add(quote);
            Task.Run(() => WriteQuotesFile(quotesPath));
        }

        async Task WriteQuotesFile(string path) {
            await using var sw = new StreamWriter(path);
            await sw.WriteAsync(JsonConvert.SerializeObject(quotes));
        }
        
        async Task ReadQuotesFile(string path) {
            using var sr = new StreamReader(path);
            var tmp = await sr.ReadToEndAsync().ConfigureAwait(false);
            quotes = JsonConvert.DeserializeObject<List<string>>(tmp);
        }
    }
}