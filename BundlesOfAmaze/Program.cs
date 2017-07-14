using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using BundlesOfAmaze.Application;
using BundlesOfAmaze.InversionOfControl;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace BundlesOfAmaze
{
    internal class Program
    {
        public IConfigurationRoot Configuration { get; private set; }
        public IContainer Container { get; private set; }

        private DiscordSocketClient _client;
        private MessageHandler _messageHandler;

        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var environmentName = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables();
            Configuration = configBuilder.Build();

            // Configure AutoFac
            var builder = new ContainerBuilder();
            AutofacConfig.Register(builder);
            Container = builder.Build();

            // Create a new instance of DiscordSocketClient.
            _client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                // Specify console verbose information level.
                LogLevel = LogSeverity.Verbose,

                // Tell discord.net how long to store messages (per channel).
                MessageCacheSize = 1000
            });

            var logger = new Logger();
            _client.Log += logger.ClientOnLogAsync;

            await _client.LoginAsync(TokenType.Bot, Configuration["Discord:Token"]);
            await _client.StartAsync();
            await _client.SetGameAsync(".amazecats help");

            using (var scope = Container.BeginLifetimeScope())
            {
                var commandService = scope.Resolve<ICommandService>();
                _messageHandler = new MessageHandler(commandService, _client);

                _client.MessageReceived += _messageHandler.HandleMessageAsync;

                // Block this task until the program is closed
                await Task.Delay(-1);
            }
        }
    }
}