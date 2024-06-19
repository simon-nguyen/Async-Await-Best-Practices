using GoComics.Core.ViewModels;

namespace GoComics.Views;

public partial class ComicView
{
    public ComicView(ComicViewModel viewModel)
    {
        BindingContext = viewModel;

        InitializeComponent();
    }
}