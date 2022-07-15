using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RegymBot.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace RegymBot.Configurations
{
    public class ConfigureWebhook : IHostedService
    {
        private readonly IServiceProvider _services;
        private readonly BotConfiguration _botConfig;
        private readonly ILogger<ConfigureWebhook> _logger;

        public ConfigureWebhook(IServiceProvider serviceProvider,
                                IConfiguration configuration,
                                ILogger<ConfigureWebhook> logger)
        {
            _services = serviceProvider;
            _botConfig = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            var webhookAddress = @$"{_botConfig.HostAddress}/bot/{_botConfig.Token}";
            _logger.LogInformation("Setting webhook: {WebhookAddress}", webhookAddress);

            await botClient.SetWebhookAsync(
                url: webhookAddress,
                allowedUpdates: Array.Empty<UpdateType>(),
                cancellationToken: cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            // Remove webhook upon app shutdown
            _logger.LogInformation("Removing webhook");
            await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
        }
    }
}
