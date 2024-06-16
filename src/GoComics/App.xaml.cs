using GoComics.Core.Http;
using GoComics.Core.Services;
using Prism.DryIoc;
using Prism.Ioc;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;

namespace GoComics
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IGoComicsService>(p =>
            {
                var comicsClient = new HttpClient();

                var service = new GoComicsService(comicsClient);
                return service;
            });
        }
    }

}
