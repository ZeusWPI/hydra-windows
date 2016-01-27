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

        // Resto pages
        public const string RestoPage = "Resto/RestoPage";
        public const string RestoMapPage = "Resto/RestoMapPage";

        // Activities pages
        public const string ActivitiesPage = "Activities/ActivitiesPage";

        // Info pages
        public const string InfoPage = "Info/InfoPage";
        public const string MinervaPage = "Info/MinervaPage";
        public const string OasisPage = "Info/OasisPage";
        public const string EduroamPage = "Info/EduroamPage";
        public const string VpnPage = "Info/VpnPage";
        public const string AcademicCalendarPage = "Info/AcademicCalendarPage";
        public const string DoctorPage = "Info/DoctorPage";
        public const string BicyclePage = "Info/BicyclePage";
        public const string BlokmapPage = "Info/BlokmapPage";

        // Schamper pages
        public const string SchamperPage = "Schamper/SchamperPage";

        // Urgent.FM pages
        public const string UrgentFmPage = "UrgentFm/UrgentFmPage";

        // Settings pages
        public const string SettingsPage = "Settings/SettingsPage";
    }
}
