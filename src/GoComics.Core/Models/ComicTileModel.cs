using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Core.Models
{
    public record ComicTileModel(string Title, string Author, string PublishDateString, string AvatarUrl)
    {
    }
}
