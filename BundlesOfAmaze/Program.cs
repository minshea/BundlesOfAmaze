﻿using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using BundlesOfAmaze.Application;
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

            // Configure AutoFac
            var builder = new ContainerBuilder();
            AutofacConfig.Register(builder, configuration.GetConnectionString("DataContext"));
            builder.RegisterInstance(configuration).As<IConfigurationRoot>().SingleInstance();
            Container = builder.Build();

            // Configure Discord
            using (_client = await ConfigureDiscordCLientAsync(configuration))
            {
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

            await client.LoginAsync(TokenType.Bot, configuration["Discord:Token"]);
            await client.StartAsync();
            await client.SetGameAsync(".amazecats help");

            client.MessageReceived += ClientOnMessageReceived;

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