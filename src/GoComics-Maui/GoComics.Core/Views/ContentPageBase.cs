using GoComics.Core.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Core.Views
{
    public class ContentPageBase : ContentPage
    {
        public ContentPageBase()
        {
            NavigationPage.SetBackButtonTitle(this, string.Empty);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is not IViewModel viewModel)
            {
                Debug.WriteLine("The current BindingContext is not an implementation of IViewModel!");
                return;
            }

            await viewModel.InitializeAsyncCommand.ExecuteAsync(null);
        }
    }
}
