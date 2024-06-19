using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoComics.Core.Services;
using System.Threading;

namespace GoComics.Core.ViewModels;

public partial class ViewModelBase : ObservableObject, IViewModel
{
    [ObservableProperty]
    private bool _isInitialized;

    [ObservableProperty]
    private string _title = default!;

    [ObservableProperty]
    private string _statusText = default!;

    private long _isBusyIndicator;

    public ViewModelBase(INavigationService navigationService)
    {
        NavigationService = navigationService;

        InitializeAsyncCommand = new AsyncRelayCommand(
            ExecuteInitializeAsyncCommand
            , AsyncRelayCommandOptions.FlowExceptionsToTaskScheduler);
    }

    public bool IsBusy => Interlocked.Read(ref _isBusyIndicator) > 0;

    public INavigationService NavigationService { get; private set; }

    public IAsyncRelayCommand InitializeAsyncCommand { get; private set; }
    private async Task ExecuteInitializeAsyncCommand()
    {
        await SetIsBusyFor(InitializeAsync);
        IsInitialized = true;
    }

    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public virtual void OnViewLoaded() { }

    protected async Task SetIsBusyFor(Func<Task> action)
    {
        Interlocked.Increment(ref _isBusyIndicator);
        OnPropertyChanged(nameof(IsBusy));

        try
        {
            await action();
        }
        finally
        {
            Interlocked.Decrement(ref _isBusyIndicator);
            OnPropertyChanged(nameof(IsBusy));
        }
    }

    #region IQueryAttributable
    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
    }
    #endregion
}
