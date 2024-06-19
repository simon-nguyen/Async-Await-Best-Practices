using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Core.Messages
{
    public class ComicCountChanged(int Count)
        : ValueChangedMessage<int>(Count)
    {
    }
}
