﻿using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using MatronBot.Commands;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MatronBot {
    public class Bot {
        
        //https://discord.com/oauth2/authorize?response_type=code&client_id=825758473219866637&scope=bot&&redirect_uri=http://yoursite.com&prompt=consent
        
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public async Task RunAsync() {

            string json;
            
            await using (var fs = File.OpenRead("config.json"))
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                    json = await sr.ReadToEndAsync()
                        .ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);
            
            var config = new DiscordConfiguration() {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
            };
            
            Client = new DiscordClient(config);
            Client.Ready += OnClientReady;
            Client.UseInteractivity(new InteractivityConfiguration {
                Timeout = TimeSpan.FromMinutes(3)
            });
            
            var commandsConfig = new CommandsNextConfiguration() 
            {
                StringPrefixes = new [] {configJson.Prefix},
                EnableDms = false,
                EnableMentionPrefix = true,
                DmHelp = true
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            
            Commands.RegisterCommands<ApexCommands>();
            Commands.RegisterCommands<DeathCounter>();
            Commands.RegisterCommands<FunCommands>();
            Commands.RegisterCommands<WarframeCommands>();
            
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private Task OnClientReady(object sender, ReadyEventArgs e) {
            return Task.CompletedTask;
        }
    }
}