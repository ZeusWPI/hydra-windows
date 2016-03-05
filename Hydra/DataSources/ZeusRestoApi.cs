using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hydra.Models.Resto;
using System.Globalization;
using System.Runtime.Serialization.Json;
using Hydra.Exceptions;

namespace Hydra.DataSources {
    public class ZeusRestoApi : RestApi, IRestoSource {

        private const string BASE_URL = "https://zeus.ugent.be";
        private const string API_PATH = "/hydra/api/2.0/resto/";

        private RestoLocation[] restoLocations;
        private Dictionary<DateTime, DailyMenu> restoMenus;
        private SandwichMenu sandwichMenu;

        public ZeusRestoApi() : base(BASE_URL, API_PATH) {
            this.restoMenus = new Dictionary<DateTime, DailyMenu>();
            this.sandwichMenu = new SandwichMenu() {
                Sandwiches = null
            };
        }

        public string getPreferredLanguage() {
            return "nl";
        }

        /// <summary>
        /// Retrieves the locations of all resto's.
        /// </summary>
        public async Task<RestoLocation[]> GetRestoLocations() {
            if(this.restoLocations == null) {
                await GetRestoMeta();
            }
            
            return this.restoLocations;
        }

        private async Task GetRestoMeta() {
            RestoMeta restoMeta = await Get<RestoMeta>("/meta.json");
            this.restoLocations = restoMeta.Locations;
        }
        
        public async Task<ICollection<DailyMenu>> GetRestoMenus(int nextDays) {
            if (restoMenus.Count > 0) {
                return restoMenus.Values;
            }

            DateTime date = DateTime.Now;
            int i = 0;
            bool dsvFuckedUp = false; // In case DSV is _really_ late filling in the menus
            while(i <= nextDays && !dsvFuckedUp) {
                date = date.AddDays(1);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday) {
                    string weekMenuApiUrl = $"/menu/{getPreferredLanguage()}/{date.Year}/{date.Month}/{date.Day}.json";
                    try {
                        restoMenus.Add(date, await Get<DailyMenu>(weekMenuApiUrl));
                    } catch(DataSourceException e) { // Error 404
                        dsvFuckedUp = true;
                    }
                    i++;
                }
            }

            return restoMenus.Values;
        }

        public async Task<SandwichMenu> GetRestoSandwichMenu() {
            if (sandwichMenu.Sandwiches == null) {
                sandwichMenu.Sandwiches = await Get<Sandwich[]>("/sandwiches.json");
            }

            return sandwichMenu;
        }
    }
}
