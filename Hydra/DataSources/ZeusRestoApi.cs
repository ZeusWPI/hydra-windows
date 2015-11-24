using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hydra.Models.Resto;

namespace Hydra.DataSources {
    public class ZeusRestoApi : RestApi, IRestoSource {

        private const string BASE_URL = "https://zeus.ugent.be";
        private const string API_PATH = "/hydra/api/1.0/resto";

        private RestoLocation[] restoLocations;
        private RestoLegendItem[] restoLegendItems;
        private List<DailyMenu> restoMenus;

        // Format: "menu/{year}/{nr of the week}.json"
        private const string RESTO_MENU_URL = "menu/{0}/{1}.json";

        public ZeusRestoApi() : base(BASE_URL, API_PATH) {
        }

        /// <summary>
        /// Retrieves the locations of all resto's.
        /// </summary>
        public async Task<IEnumerable<RestoLocation>> GetRestoLocations() {
            if(restoLocations == null) {
                await GetRestoMeta();
            }
            
            return restoLocations;
        }

        /// <summary>
        /// Retrieves the legend for the resto menus.
        /// </summary>
        public async Task<IEnumerable<RestoLegendItem>> GetRestoLegendItems() {
            if (restoLegendItems == null) {
                await GetRestoMeta();
            }

            return restoLegendItems;
        }

        private async Task GetRestoMeta() {
            RestoMeta restoMeta = await Get<RestoMeta>("/meta.json");
            restoLegendItems = restoMeta.Legend;
            restoLocations = restoMeta.Locations;
        }

        public Task<IEnumerable<DailyMenu>> GetRestoMenusThisWeek() {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DailyMenu>> GetRestoMenus(int nextDays) {
            throw new NotImplementedException();
        }
    }
}
