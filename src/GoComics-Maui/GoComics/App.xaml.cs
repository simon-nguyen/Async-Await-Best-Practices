using CommunityToolkit.Mvvm.DependencyInjection;
using GoComics.Core.Routings;
using GoComics.Core.Services;
using GoComics.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.ApplicationModel;

namespace GoComics;

public partial class App : Application
{
    private readonly ISettingsService _settingsService;
    private readonly INavigationService _navigationService;

    public App(ISettingsService settingsService
        , INavigationService navigationService)
    {
        _settingsService = settingsService;
        _navigationService = navigationService;

        InitializeComponent();

        if (VersionTracking.IsFirstLaunchEver)
        {
            // TODO: handle when this is the first time the app launches
        }

        MainPage = new AppShell(navigationService);
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
