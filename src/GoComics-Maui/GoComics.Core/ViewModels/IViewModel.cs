using CommunityToolkit.Mvvm.Input;
using GoComics.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Core.ViewModels
{
    public interface IViewModel : IQueryAttributable
    {
        INavigationService NavigationService { get; }

        bool IsInitialized { get; }
        bool IsBusy { get; }

        IAsyncRelayCommand InitializeAsyncCommand { get; }
        Task InitializeAsync();
    }
}
