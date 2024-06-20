using CommunityToolkit.Mvvm.DependencyInjection;
using GoComics.Core.Routings;
using GoComics.Core.Services;
using GoComics.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.ApplicationModel;

namespace GoComics;

public partial class App : Application
{
    private readonly INavigationService _navigationService;
    private readonly ILogger<App> _logger;

    public App(INavigationService navigationService, ILoggerFactory loggerFactory)
    {
        _navigationService = navigationService;
        _logger = loggerFactory.CreateLogger<App>();

        InitializeComponent();

        if (VersionTracking.IsFirstLaunchEver)
        {
            // TODO: handle when this is the first time the app launches
        }

        var shellLogger = loggerFactory.CreateLogger<AppShell>();
        MainPage = new AppShell(navigationService, shellLogger);
    }

    protected override void OnStart()
    {
        base.OnStart();

        InitStatusBar();
    }

    protected override void OnSleep()
    {
        base.OnSleep();

        SetStatusBar();
        RequestedThemeChanged -= AppOnRequestedThemeChanged;
    }

    protected override void OnResume()
    {
        base.OnResume();

        InitStatusBar();
    }

    private void AppOnRequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
    {
        Dispatcher.Dispatch(() => SetStatusBar());
    }

    private void InitStatusBar()
    {
        SetStatusBar();
        RequestedThemeChanged += AppOnRequestedThemeChanged;
    }

    private void SetStatusBar()
    {
        var nav = Current!.MainPage as NavigationPage;

        if (Current.RequestedTheme == AppTheme.Dark)
        {
            if (nav is not null)
            {
                nav.BarBackgroundColor = Colors.Black;
                nav.BarTextColor = Colors.White;
            }
        }
        else
        {
            if (nav is not null)
            {
                nav.BarBackgroundColor = Colors.White;
                nav.BarTextColor = Colors.Black;
            }
        }
    }

    public static void HandleAppActions(AppAction action)
    {
        if (Current is not App app)
        {
            return;
        }

        app.Dispatcher.Dispatch(async () =>
        {
            if (action.Id.Equals(WellknownAppActions.ViewProfileAction.Id))
            {
                await app._navigationService.NavigateToAsync(WellknowRoutes.ProfileRouteUri);
            }
        });
    }
}
