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
        public const string NewsPage = "News/NewsPage";
        public const string RestoPage = "Resto/RestoPage";
        public const string ActivitiesPage = "Activities/ActivitiesPage";

        // Info pages
        public const string InfoPage = "Info/InfoPage";
        public const string MinervaPage = "Info/MinervaPage";
        public const string OasisPage = "Info/OasisPage";
        public const string AcademicCalendarPage = "Info/AcademicCalendarPage";
    }
}
