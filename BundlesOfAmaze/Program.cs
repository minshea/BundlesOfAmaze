using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BundlesOfAmaze.Application;
using BundlesOfAmaze.Data;
using BundlesOfAmaze.InversionOfControl;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Hangfire;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;

namespace BundlesOfAmaze
{
    internal class Program
    {
        private AutofacServiceProvider _serviceProvider;
        private static IContainer _container;
        private DiscordSocketClient _client;
        private CommandService _commandService;
        private IConfigurationRoot _configuration;

        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var environmentName = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            Console.WriteLine($"Starting BundlesOfAmaze in {environmentName} mode...");

            // Load configuration
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables();
            _configuration = configBuilder.Build();

            Console.WriteLine("Build number: {0}", _configuration["BuildNumber"]);
            Console.WriteLine("Connection string: {0}", _configuration.GetConnectionString("DataContext"));
            Console.WriteLine("Discord Token: {0}", _configuration["Discord:Token"]);

            // Configure application insights
            TelemetryConfiguration.Active.InstrumentationKey = _configuration["ApplicationInsights:InstrumentationKey"];
            if (environmentName != "production")
            {
                TelemetryConfiguration.Active.TelemetryChannel.DeveloperMode = true;
            }

            // Configure command service
            _commandService = new CommandService();
            _commandService.Log += HandleLog;
            await _commandService.AddModulesAsync(typeof(HelpModule).GetTypeInfo().Assembly);

            // Configure and connect to Discord
            using (_client = await ConfigureDiscordCLientAsync(_configuration))
            {
                // Configure AutoFac
                var builder = new ContainerBuilder();
                AutofacConfig.Register(builder, _configuration.GetConnectionString("DataContext"));
                builder.RegisterInstance(_configuration).As<IConfigurationRoot>().SingleInstance();
                builder.RegisterInstance(_client).As<IDiscordClient>().SingleInstance();
                builder.RegisterInstance(_commandService).As<CommandService>().SingleInstance();
                _container = builder.Build();
                _serviceProvider = new AutofacServiceProvider(_container);

                // Seed data
                using (var scope = _container.BeginLifetimeScope())
                {
                    Console.Write("Seeding database... ");
                    var dataContext = scope.Resolve<IDataContext>();
                    await dataContext.SeedAsync();
                    Console.WriteLine("OK");
                }

                // Configure Hangfire
                GlobalConfiguration.Configuration.UseSqlServerStorage(_configuration.GetConnectionString("DataContext"));
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
            Console.WriteLine("Connecting to Discord... ");

            // Create a new instance of DiscordSocketClient.
            var client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                // Specify console verbose information level.
                LogLevel = LogSeverity.Verbose,

                // Tell discord.net how long to store messages (per channel).
                MessageCacheSize = 100
            });

            client.Log += HandleLog;
            client.MessageReceived += HandleCommandAsync;

            await client.LoginAsync(TokenType.Bot, configuration["Discord:Token"]);
            await client.StartAsync();

            var prefix = GetPrefix();
            await client.SetGameAsync($"{prefix} help");

            return client;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            try
            {
                // Don't process the command if it was a System Message
                var message = s as SocketUserMessage;
                if (message == null)
                {
                    return;
                }

                var argPos = 0;
                var prefix = GetPrefix();

                // Check if the message has either a string or mention prefix
                if (message.HasStringPrefix(prefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
                {
                    using (_container.BeginLifetimeScope())
                    {
                        var telemetry = _container.Resolve<ITelemetryService>();
                        var client = telemetry.Client;
                        using (var operation = telemetry.Client.StartOperation<RequestTelemetry>("HandleCommand"))
                        {
                            // Initialize the owner service
                            var authorId = message.Author.Id.ToString();
                            var owner = await _container.Resolve<IOwnerRepository>().FindByAuthorIdAsync(authorId);
                            var ownerService = _container.Resolve<IOwnerService>();
                            ownerService.SetCurrentOwner(owner);

                            if (owner != null)
                            {
                                client.Context.User.Id = authorId;
                            }

                            var context = new SocketCommandContext(_client, message);

                            IResult result;
                            if (message.Content.Trim() == prefix)
                            {
                                // Not sure how to handle a command with only the prefix yet
                                result = await _commandService.ExecuteAsync(context, "overview", _serviceProvider);
                            }
                            else
                            {
                                // Try and execute a command with the given context
                                result = await _commandService.ExecuteAsync(context, argPos + 1, _serviceProvider);
                            }

                            // If execution failed, reply with the error message
                            if (!result.IsSuccess)
                            {
                                await context.Channel.SendMessageAsync(result.ToString());
                            }

                            // Set properties of containing telemetry item--for example:
                            operation.Telemetry.ResponseCode = "200";

                            // Optional: explicitly send telemetry item:
                            client.StopOperation(operation);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public async Task Tick()
        {
            try
            {
                using (var scope = _container.BeginLifetimeScope())
                {
                    var backgroundService = scope.Resolve<IBackgroundService>();
                    await backgroundService.HandleTickAsync();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private Task HandleLog(LogMessage logMessage)
        {
            Console.WriteLine(logMessage);

            // Log all errors and criticals
            if (logMessage.Severity < LogSeverity.Warning)
            {
                var telemetry = _container.Resolve<ITelemetryService>();
                telemetry.Client.TrackException(logMessage.Exception);
            }

            return Task.FromResult(0);
        }

        private string GetPrefix()
        {
            return _configuration["NETCORE_ENVIRONMENT"] == "production" ? Commands.Prefix : Commands.PrefixDev;
        }
    }
}