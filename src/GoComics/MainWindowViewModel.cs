using GoComics.Core.Models;
using GoComics.Core.Mvvm;
using GoComics.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGoComicsService _service;

        public MainWindowViewModel(IGoComicsService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public ObservableCollection<ComicTileModel> ComicsCollection { get; private set; } = new();

        public async void OnViewLoaded()
        {
            var twoSecondsDelayTask = Task.Delay(TimeSpan.FromSeconds(2));

            try
            {
                await foreach (var comic in _service.GetAllComicsAsync())
                {
                    ComicsCollection.Add(comic);
                }
            }
            finally
            {
                await twoSecondsDelayTask;
            }
        }
    }
}
