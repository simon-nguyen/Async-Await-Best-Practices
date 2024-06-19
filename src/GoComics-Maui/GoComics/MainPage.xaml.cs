using CommunityToolkit.Mvvm.Messaging;
using GoComics.Core.Messages;
using GoComics.Core.ViewModels;
using GoComics.Core.Views;

namespace GoComics;

public partial class MainPage
{
    private readonly MainViewModel _viewModel;

    public MainPage(MainViewModel vm)
    {
        BindingContext = _viewModel = vm;

        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        WeakReferenceMessenger.Default.Register<MainPage, ComicCountChanged>(
            this,
            async (recipient, message) =>
            {
                await recipient.Dispatcher.DispatchAsync(async () =>
                {
                    await recipient.Content.ScaleTo(1.2);
                    await recipient.Content.ScaleTo(1.0);
                });
            });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        WeakReferenceMessenger.Default.Unregister<ComicCountChanged>(this);
    }
}
