using GoComics.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Core.ViewModels;

public partial class LoginViewModel(
    INavigationService navigationService
    ) : ViewModelBase(navigationService)
{
    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        base.ApplyQueryAttributes(query);
    }
}
