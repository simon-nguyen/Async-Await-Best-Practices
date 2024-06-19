﻿using GoComics.Core.Services;

namespace GoComics;

public partial class AppShell : Shell
{
    private readonly INavigationService _navigationService;

    public AppShell(INavigationService navigationService)
    {
        _navigationService = navigationService;

        InitializeComponent();
    }

    protected override async void OnParentSet()
    {
        base.OnParentSet();

        if (Parent is not null)
            await _navigationService.InitializeAsync();
    }
}
