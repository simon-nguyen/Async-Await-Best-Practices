using GoComics.Core.Models;
using GoComics.Core.Mvvm;
using GoComics.Core.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GoComics
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly CancellationTokenSource _cts = new();
        private readonly IGoComicsService _service;

        public MainWindowViewModel(IGoComicsService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        private string _statusText = default!;

        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }

        public ObservableCollection<ComicTileModel> ComicsCollection { get; private set; } = new();

        private ICommand _cancelCommand;

        public ICommand CancelCommand => _cancelCommand ??= new DelegateCommand<object>(ExecuteCancelCommand);

        private void ExecuteCancelCommand(object parameter)
        {
            _cts.Cancel();
        }


        public async void OnViewLoaded()
        {
            var twoSecondsDelayTask = Task.Delay(TimeSpan.FromSeconds(2));

            try
            {
                await foreach (var comic in _service.GetAllComicsAsync(_cts.Token))
                {
                    StatusText = comic.Title;
                    ComicsCollection.Add(comic);
                }
            }
            catch (OperationCanceledException)
            {
                StatusText = "Canceled.";
            }
            finally
            {
                await twoSecondsDelayTask;
            }
        }
    }
}
