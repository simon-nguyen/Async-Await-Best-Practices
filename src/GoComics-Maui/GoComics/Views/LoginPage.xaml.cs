using GoComics.Core.ViewModels;

namespace GoComics.Views;

public partial class LoginPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        BindingContext = viewModel;

        InitializeComponent();
    }
}