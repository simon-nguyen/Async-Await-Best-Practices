using GoComics.Core.Routings;
using GoComics.Core.Services;
using GoComics.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Services;

public class NavigationService(ISettingsService _settings) : INavigationService
{
    public Task InitializeAsync()
    {
        //Routing.RegisterRoute(WellknowRoutes.LoginRoute, typeof(LoginPage));
        //Routing.RegisterRoute(WellknowRoutes.ComicsRoute, typeof(MainPage));
        //Routing.RegisterRoute(WellknowRoutes.ComicRoute, typeof(ComicView));

        var route = string.IsNullOrEmpty(_settings.AuthAccessToken)
            //? WellknowRoutes.LoginRouteUri
            ? WellknowRoutes.ComicsRouteUri
            : WellknowRoutes.ComicsRouteUri;

        return NavigateToAsync(route);
    }

    public Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null)
    {
        var state = new ShellNavigationState(route);

        return parameters is not null
            ? Shell.Current.GoToAsync(state, parameters)
            : Shell.Current.GoToAsync(state);
    }
}
