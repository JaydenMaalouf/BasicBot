using BasicBot.Classes;
using BasicBot.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BasicBot
{
    class Program
    {
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient discordClient;

        public async Task MainAsync()
        {
            Console.WriteLine("Hi");

            discordClient = new DiscordSocketClient(new DiscordSocketConfig() { LogLevel = LogSeverity.Verbose } );

            var services = ConfigureServices();
            await services.GetRequiredService<CommandHandlerService>().InitializeAsync();

            var token = SettingsHandler.GetSettings().BotToken;

            await discordClient.LoginAsync(TokenType.Bot, token);
            await discordClient.StartAsync();

            await Task.Delay(-1);
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(discordClient)
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlerService>()
                .BuildServiceProvider();
        }
    }
}
