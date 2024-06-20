using GoComics.Core.Services;
using Microsoft.Extensions.Logging;

namespace GoComics;

public partial class AppShell : Shell
{
    private readonly INavigationService _navigationService;
    private readonly ILogger<AppShell> _logger;

    public AppShell(INavigationService navigationService, ILogger<AppShell> logger)
    {
        _navigationService = navigationService;
        _logger = logger;

        InitializeComponent();
    }

    protected override async void OnParentSet()
    {
        base.OnParentSet();

        if (Parent is not null)
            await _navigationService.InitializeAsync();
    }
}
