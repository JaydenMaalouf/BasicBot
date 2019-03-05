using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasicBot.Services
{
    public class CommandHandlerService
    {
        private readonly DiscordSocketClient discord;
        private readonly CommandService commands;
        private readonly IServiceProvider provider;

        public async Task InitializeAsync()
        {
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), provider);
        }

        public CommandHandlerService(IServiceProvider _provider, DiscordSocketClient _discord, CommandService _commands)
        {
            provider = _provider;
            discord = _discord;
            commands = _commands;

            discord.MessageReceived += Discord_MessageReceived;
            discord.Log += Discord_Log;
        }

        private Task Discord_Log(LogMessage arg)
        {
            Console.WriteLine(arg.Message);
            return Task.CompletedTask;
        }

        private async Task Discord_MessageReceived(SocketMessage socketMessage)
        {
            if (socketMessage is IUserMessage message)
            {
                if (socketMessage.Author.IsBot)
                {
                    return;
                }

                var context = new CommandContext(discord, message);

                var argPos = 0;
                if (message.HasCharPrefix('?', ref argPos))
                {
                    var result = await commands.ExecuteAsync(context, argPos, provider);
                    if (result.Error != null)
                    {
                        //DO STUFF HERE
                    }
                }
            }
        }
    }
}
