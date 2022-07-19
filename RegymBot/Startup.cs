using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RegymBot.Configurations;
using RegymBot.Data;
using Telegram.Bot;
using Microsoft.EntityFrameworkCore;
using RegymBot.Handlers;
using RegymBot.Data.Repositories;
using RegymBot.Helpers;
using Microsoft.Extensions.Logging;
using RegymBot.Services;
using RegymBot.Services.Impl;

namespace RegymBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            BotConfig = Configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
        }

        public IConfiguration Configuration { get; }
        private BotConfiguration BotConfig { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // register loggers
            services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<ConfigureWebhook>>());
            services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<CallbackQuery>>());
            services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<HandleUpdate>>());
            services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<HandleMainMenu>>());
            services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<HandleError>>());
            services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<IStepService>>());

            services.AddDbContext<AppDbContext>(opt => 
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHostedService<ConfigureWebhook>();

            services.AddHttpClient("tgwebhook")
                    .AddTypedClient<ITelegramBotClient>(httpClient
                        => new TelegramBotClient(BotConfig.Token, httpClient));

            // register handlers for Bot
            services.AddScoped<HandleUpdate>();
            services.AddScoped<HandleMainMenu>();
            services.AddScoped<CallbackQuery>();
            services.AddScoped<HandleError>();

            // register services
            services.AddScoped<IStepService, StepService>();

            // register repositories
            services.AddScoped<PriceRepository>();
            services.AddScoped<StaticMessageRepository>();

            services.AddControllers().AddNewtonsoftJson();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                var token = BotConfig.Token;
                endpoints.MapControllerRoute(name: "tgwebhook",
                                             pattern: $"bot/{token}",
                                             new { controller = "Webhook", action = "Post" });
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
