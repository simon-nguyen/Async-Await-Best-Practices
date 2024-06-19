using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Core.Services;

public interface ISettingsService
{
    string AuthAccessToken { get; set; }
}

public class SettingsService : ISettingsService
{
    public string AuthAccessToken { get; set; } = default!;
}
