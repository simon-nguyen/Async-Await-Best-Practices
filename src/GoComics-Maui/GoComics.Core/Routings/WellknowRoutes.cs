using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoComics.Core.Routings
{
    public static class WellknowRoutes
    {
        public static readonly string LoginRoute = "Login";
        public static readonly string LoginRouteUri = "//Login";

        public static readonly string MainRoute = "Main";
        public static readonly string MainRouteUri = "//Main";
        public static readonly string ComicsRoute = "Comics";
        //public static readonly string ComicsRouteUri = "//Main/Comics";
        public static readonly string ComicsRouteUri = "//Comics";
        public static readonly string ComicRoute = "Comic";
        //public static readonly string ComicRouteUri = "//Main/Comic";
        public static readonly string ComicRouteUri = "//Comic";

        public static readonly string ProfileRouteUri = "//Main/Profile";
    }
}
