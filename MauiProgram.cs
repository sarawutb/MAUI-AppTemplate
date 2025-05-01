using MAUIPos.Application;
using MAUIPos.Application.Services;
using MAUIPos.Application.ViewModels;
using MAUIPos.Application.Views.Screen;
using MAUIPos.Application.Views.Widget;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Mopups.Hosting;
using SkiaSharp.Views.Maui.Controls.Hosting;
using UraniumUI;
using DotNet.Meteor.HotReload.Plugin;

namespace MAUIPos
{
    public static class MauiProgram
    {
        public static IServiceProvider Services { get; private set; }
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
             .UseMauiApp<App>()
#if DEBUG
             .EnableHotReload()
#endif
             .UseSkiaSharp()
             .UseUraniumUI()
             .UseUraniumUIMaterial()
             .UseMauiCommunityToolkit()
             .UseMauiCompatibility()
             .RegisterServices()
             .RegisterViewModels()
             .RegisterViews()
             .RegisterRoute()
             .ConfigureMopups()
             .ConfigureFonts(fonts =>
             {
                 fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                 fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                 fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                 fonts.AddFont("NotoSansThai-All.ttf", "NotoSansThai");
                 fonts.AddMaterialSymbolsFonts();
                 fonts.AddFontAwesomeIconFonts();
             });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            Services = app.Services;
            return app;
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            var appConfig = new AppConfigService();
            appConfig.LoadConfigAsync().Wait();
            mauiAppBuilder.Services.AddSingleton(appConfig);
            mauiAppBuilder.Services.AddScoped<WebSocketService>();
            mauiAppBuilder.Services.AddSingleton<CacheSystemService>();
            mauiAppBuilder.Services.AddScoped<AuthService>();
            mauiAppBuilder.Services.AddSingleton<LoadingService>();
            mauiAppBuilder.Services.AddCommunityToolkitDialogs();
            mauiAppBuilder.Services.AddMopupsDialogs();
            // mauiAppBuilder.Services.AddTransient<ISettingsService, SettingsService>();
            // More services registered here.
            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<AppShellViewModel>();
            mauiAppBuilder.Services.AddSingleton<MainViewModel>();
            mauiAppBuilder.Services.AddScoped<ChatViewModel>();
            mauiAppBuilder.Services.AddScoped<ChatRoomViewModel>();
            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<LoginView>();
            mauiAppBuilder.Services.AddSingleton<AppShell>();
            mauiAppBuilder.Services.AddScoped<LoadingWidget>();
            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterRoute(this MauiAppBuilder mauiAppBuilder)
        {
            Route.RegisterRoute();
            return mauiAppBuilder;
        }
    }
}
