using CommunityToolkit.Mvvm.ComponentModel;
using GoComics.Core.Models;
using GoComics.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Core.ViewModels;

public partial class ComicViewModel(
    INavigationService navigationService
    ) : ViewModelBase(navigationService)
{
    [ObservableProperty]
    public ComicTileModel _model = default!;

    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        base.ApplyQueryAttributes(query);

        if (query.TryGetValue("Comic", out object? value) && value is ComicTileModel comic)
        {
            Model = comic;
        }
    }

    //private string _title;
    //public string Title
    //{
    //    get => Model.Title;
    //    set
    //    {
    //        _title = value;
    //        base.OnPropertyChanged(nameof(Title));
    //    }
    //}

    //[ObservableProperty]
    //private string _title = Model;

    //[ObservableProperty]
    //private string _author;
}
