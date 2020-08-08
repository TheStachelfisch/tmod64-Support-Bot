using Discord;
using Discord.WebSocket;
using SupportBot.Handler;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SupportBot
{
    class Program
    {
        private static string _token = File.ReadAllText(@"token.txt");

        private DiscordSocketClient _client;
        private DiscordSocketConfig _config;

        private CommandHandler _commandHandler;

        static void Main(string[] args)
            => new Program().StartBotAsync().GetAwaiter().GetResult();


        private async Task StartBotAsync()
        {
            _client = new DiscordSocketClient();
            _config = new DiscordSocketConfig { MessageCacheSize = 100 };

            await _client.LoginAsync(TokenType.Bot, _token);

            await _client.StartAsync();
            await _client.SetGameAsync(".help");

            _client.Log += Log;
            _client.Ready += ReadyEvent;

            await Task.Delay(-1);
        }

        //Add Handlers here
        private async Task ReadyEvent()
        {
            _commandHandler = new CommandHandler(_client);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
    }
}
