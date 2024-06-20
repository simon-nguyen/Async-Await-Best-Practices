using CommunityToolkit.Mvvm.ComponentModel;
using GoComics.Core.Models;
using GoComics.Core.Services;
using GoComics.LookAndFeel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Core.ViewModels;

public partial class ComicViewModel(
    INavigationService navigationService
    , ImageDownloadService _imageDownloadService)
    : ViewModelBase(navigationService)
{
    [ObservableProperty]
    private ComicTileModel _model = default!;

    [ObservableProperty]
    private ImageSource _avatar = Images.GoComicsGray;

    public override async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        base.ApplyQueryAttributes(query);

        if (query.TryGetValue("Comic", out object? value) && value is ComicTileModel comic)
        {
            Model = comic;
            if (!string.IsNullOrWhiteSpace(comic.AvatarUrl))
            {
                Avatar = await SetIsBusyFor(DownloadAvatar);
            }
        }
    }

    private async ValueTask<ImageSource> DownloadAvatar()
    {
        try
        {
            // TODO: cache the image by saving the downloaded stream to local/sqlite, then load it by key
            using var imageStream = await _imageDownloadService.DownloadImageStreamAsync(Model.AvatarUrl!);
            return ImageSource.FromStream(() => imageStream);
        }
        catch (Exception)
        {
            throw;
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
