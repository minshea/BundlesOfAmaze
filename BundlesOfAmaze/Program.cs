using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using BundlesOfAmaze.Application;
using BundlesOfAmaze.Data;
using BundlesOfAmaze.InversionOfControl;
using Discord;
using Discord.WebSocket;
using Hangfire;
using Microsoft.Extensions.Configuration;

namespace BundlesOfAmaze
{
    internal class Program
    {
        public static IContainer Container;
        private DiscordSocketClient _client;

        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var environmentName = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            // Load configuration
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables();
            var configuration = configBuilder.Build();

            Console.WriteLine("Starting BundlesOfAmaze...");
            Console.WriteLine("Connection string: {0}", configuration.GetConnectionString("DataContext"));
            Console.WriteLine("Discord Token: {0}", configuration["Discord:Token"]);

            // Configure and connect to Discord
            using (_client = await ConfigureDiscordCLientAsync(configuration))
            {
                // Configure AutoFac
                var builder = new ContainerBuilder();
                AutofacConfig.Register(builder, configuration.GetConnectionString("DataContext"));
                builder.RegisterInstance(configuration).As<IConfigurationRoot>().SingleInstance();
                builder.RegisterInstance(_client).As<IDiscordClient>().SingleInstance();
                Container = builder.Build();

                // Seed data
                using (var scope = Container.BeginLifetimeScope())
                {
                    var dataContext = scope.Resolve<IDataContext>();
                    Console.Write("Seeding database... ");
                    await dataContext.SeedAsync();
                    Console.WriteLine("OK");
                }

                // Configure Hangfire
                GlobalConfiguration.Configuration.UseSqlServerStorage(configuration.GetConnectionString("DataContext"));
                RecurringJob.AddOrUpdate("Tick", () => Tick(), Cron.Minutely);

                using (new BackgroundJobServer())
                {
                    // Block this task until the program is closed
                    await Task.Delay(-1);
                }
            }
        }

        private async Task<DiscordSocketClient> ConfigureDiscordCLientAsync(IConfigurationRoot configuration)
        {
            Console.Write("Connecting to Discord... ");

            // Create a new instance of DiscordSocketClient.
            var client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                // Specify console verbose information level.
                LogLevel = LogSeverity.Verbose,

                // Tell discord.net how long to store messages (per channel).
                MessageCacheSize = 100
            });

            var logger = new Logger();
            client.Log += logger.ClientOnLogAsync;

            var prefix = configuration["NETCORE_ENVIRONMENT"] == "production" ? Commands.Prefix : Commands.PrefixDev;

            await client.LoginAsync(TokenType.Bot, configuration["Discord:Token"]);
            await client.StartAsync();
            await client.SetGameAsync($"{prefix} help");

            client.MessageReceived += ClientOnMessageReceived;

            Console.WriteLine("OK");

            return client;
        }

        private async Task ClientOnMessageReceived(SocketMessage socketMessage)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var commandService = scope.Resolve<ICommandService>();
                var messageHandler = new MessageHandler(commandService, _client);

                await messageHandler.HandleMessageAsync(socketMessage);
            }
        }

        public async Task Tick()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var backgroundService = scope.Resolve<IBackgroundService>();
                await backgroundService.HandleTickAsync();
            }
        }
    }
}