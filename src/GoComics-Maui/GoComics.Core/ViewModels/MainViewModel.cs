using CommunityToolkit.Mvvm.Input;
using GoComics.Core.Models;
using GoComics.Core.Routings;
using GoComics.Core.Services;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GoComics.Core.ViewModels;

public partial class MainViewModel: ViewModelBase
{
    private readonly GoComicsService _service;
    private readonly ILogger<MainViewModel> _logger;

    protected readonly CancellationTokenSource _cts = new();

    public MainViewModel(INavigationService navigationService
        , GoComicsService service
        , ILogger<MainViewModel> logger)
        : base(navigationService)
    {
        _service = service;
        _logger = logger;
    }

    public ObservableCollection<ComicTileModel> ComicCollection { get; private set; } = new();

    [RelayCommand(CanExecute = nameof(IsBusy))]
    private void Cancel(object parameter)
    {
        _cts.Cancel();
    }

    [RelayCommand]
    private Task TapComicTile(ComicTileModel comic)
    {
        var parameters = new Dictionary<string, object>
        {
            { "Comic", comic }
        };

        return NavigationService.NavigateToAsync(WellknowRoutes.ComicRoute, parameters);
    }

    public override async Task InitializeAsync()
    {
        //var twoSecondsDelayTask = Task.Delay(TimeSpan.FromSeconds(2)); 
        try
        {
            await foreach (var comic in _service.GetAllComicsAsync(_cts.Token))
            {
                StatusText = comic.Title;
                ComicCollection.Add(comic);
            }
        }
        catch (OperationCanceledException)
        {
            StatusText = "Canceled.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        finally
        {
            //await twoSecondsDelayTask; 
        }
    }
}
