﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using tMod64Bot.Services.Config;
using tMod64Bot.Services.Logging;

namespace tMod64Bot.Services
{
    public sealed class CommandHandler : ServiceBase
    {
        private static readonly CommandError IGNORED_ERRORS = CommandError.BadArgCount | CommandError.UnknownCommand | CommandError.UnmetPrecondition | CommandError.ObjectNotFound;

        private readonly CommandService _commands;
        private readonly ConfigService _config;
        private readonly LoggingService _loggingService;

        public CommandHandler(IServiceProvider services) : base(services)
        {
            _commands = services.GetRequiredService<CommandService>();
            
            _config = services.GetRequiredService<ConfigService>();
        }
        
        public async Task InitializeAsync() => Client.MessageReceived += HandleCommandAsync;

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            if (!(arg is SocketUserMessage msg))
                return;

            var context = new SocketCommandContext(Client, msg);

            var argPos = 0;
            if (msg.HasStringPrefix(_config.Config.BotPrefix, ref argPos) || msg.HasMentionPrefix(Client.CurrentUser, ref argPos))
                await _commands.ExecuteAsync(context, argPos, Services);
        }
    }
}
