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
using RegymBot.Services;
using RegymBot.Services.Impl;
using RegymBot.Handlers.MainMenu;
using RegymBot.Handlers.ClubList;
using RegymBot.Handlers.ClubContacts;
using RegymBot.Handlers.Massage;
using RegymBot.Handlers.Price;
using RegymBot.Handlers.Solarium;
using RegymBot.Handlers.Feedback;
using RegymBot.Handlers.CategorySection;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace RegymBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Env = env;
            Configuration = configuration;
            BotConfig = Configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
        }

        public IWebHostEnvironment Env { get; }
        public IConfiguration Configuration { get; }
        private BotConfiguration BotConfig { get; }

        // This method gets called by the runtime. Use this method to add services to the container. 
        public void ConfigureServices(IServiceCollection services)
        {
            if (Env.IsDevelopment())
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                    // options.UseInMemoryDatabase("TestDB");
                });

                services.AddHostedService<ConfigureWebhook>();
                services.AddHttpClient("tgwebhook")
                        .AddTypedClient<ITelegramBotClient>(httpClient
                            => new TelegramBotClient(BotConfig.Token, httpClient));
            }
            else
            {
                services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("RemoteConnection")));

                services.AddHostedService<ConfigureWebhook>();
                services.AddHttpClient("tgwebhook")
                        .AddTypedClient<ITelegramBotClient>(httpClient
                            => new TelegramBotClient(BotConfig.Token, httpClient));
            }

            // register handlers for Bot
            services.AddScoped<HandleUpdate>();
            services.AddScoped<HandleMainMenu>();
            services.AddScoped<HandleClubList>();
            services.AddScoped<HandleClubContacts>();
            services.AddScoped<HandleFeedback>();
            services.AddScoped<HandleError>();

            services.AddScoped<CallbackQueryMainMenu>();
            services.AddScoped<CallbackQueryClubList>();
            services.AddScoped<CallbackQueryClubContacts>();
            services.AddScoped<CallbackQueryMassage>();
            services.AddScoped<CallbackQueryPrice>();
            services.AddScoped<CallbackQuerySolarium>();
            services.AddScoped<CallbackQueryFeedback>();
            services.AddScoped<InlineQueryCategorySection>();
            services.AddScoped<CallbackQueryCategorySection>();

            // register services
            services.AddSingleton<IStepService, StepService>();

            // register repositories
            services.AddScoped<PriceRepository>();
            services.AddScoped<StaticMessageRepository>();
            services.AddScoped<FeedbackRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<UserRoleRepository>();

            services.AddControllers().AddNewtonsoftJson();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // register AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                var botClient = new TelegramBotClient(BotConfig.Token);

                //botClient.StartReceiving<LongPollingService>();

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                // app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseCors(x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );

                // app.UseTelegramBotLongPolling(...);
            }
            else
            {
                app.UseSpaStaticFiles();
                // app.UseTelegramBotWebhook(...);
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                var token = BotConfig.Token;
                endpoints.MapControllerRoute(name: "tgwebhook",
                                            pattern: $"bot/{token}",
                                            new { controller = "Webhook", action = "Post" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
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
