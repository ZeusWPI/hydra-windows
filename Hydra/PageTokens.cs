using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra {
    /// <summary>
    /// Provides path-independent references to pages (to use with e.g. NavigationService).
    /// </summary>
    public static class PageTokens {
        public const string HomePage = "Home/HomePage";
        public const string RestoPage = "Resto/RestoPage";
        public const string ActivitiesPage = "Activities/ActivitiesPage";
    }
}
