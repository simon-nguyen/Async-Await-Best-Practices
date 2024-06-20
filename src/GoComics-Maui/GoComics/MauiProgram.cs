using CommunityToolkit.Maui;
using GoComics.Core.Services;
using GoComics.Core.ViewModels;
using GoComics.Views;
using Microsoft.Extensions.Logging;

namespace GoComics;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiCommunityToolkit()
            .ConfigureEssentials(essentials =>
            {
                essentials.AddAppAction(WellknownAppActions.ViewProfileAction).OnAppAction(App.HandleAppActions);
            });


        builder.Services.AddSingleton<ISettingsService, SettingsService>();
        builder.Services.AddSingleton<INavigationService, Services.NavigationService>();

#if DEBUG
        builder.Services.AddKeyedSingleton<HttpMessageHandler>("DebugHttpMessageHandler", (services, key) =>
        {
#if ANDROID
            var handler = new Xamarin.Android.Net.AndroidMessageHandler();
            handler.ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) =>
            {
                if (cert is not null && cert.Issuer.Equals("CN=localhost", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                return errors == System.Net.Security.SslPolicyErrors.None;
            };

            return handler;
#elif IOS || MACCATALYST
            var handler = new NSUrlSessionHandler
            {
                TrustOverrideForUrl = (sender, url, trust) => url.StartsWith("https://localhost", StringComparison.OrdinalIgnoreCase)
            };

            return handler;
#else
            return null;
#endif
        });

        builder.Logging.AddDebug();
#endif

        builder.Services.AddHttpClient<GoComicsService>("Comic", client => client.BaseAddress = new Uri("https://www.gocomics.com/"));
        builder.Services.AddHttpClient<ImageDownloadService>("ImageDownload");

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<ComicViewModel>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<ComicView>();

        return builder.Build();
    }
}
