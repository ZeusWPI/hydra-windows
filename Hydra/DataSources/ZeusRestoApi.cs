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

        public async Task<DailyMenu> GetRestoMenu(DateTime date) {
            // No menu in the weekend
            if (date.DayOfWeek == DayOfWeek.Saturday && date.DayOfWeek == DayOfWeek.Sunday)
                return null;

            // Check if it was already added some time in the past
            if (restoMenus.ContainsKey(date))
                return restoMenus[date];

            string weekMenuApiUrl = $"/menu/{getPreferredLanguage()}/{date.Year}/{date.Month}/{date.Day}.json";
            DailyMenu menu = await Get<DailyMenu>(weekMenuApiUrl);
            restoMenus.Add(date, menu);

            return menu;
        }

        public async Task<DailyMenu> GetRestoMenu() {
            DateTime today = DateTime.Now.Date;

            // No menu in the weekend
            if (today.DayOfWeek == DayOfWeek.Saturday)
                return await GetRestoMenu(today.AddDays(2));
            if (today.DayOfWeek == DayOfWeek.Sunday)
                return await GetRestoMenu(today.AddDays(1));

            return await GetRestoMenu(today);
        }
        
        public async Task<ICollection<DailyMenu>> GetRestoMenus(int nextDays) {
            List<DailyMenu> menus = new List<DailyMenu>(nextDays);

            DateTime date = DateTime.Now;
            int i = 0;
            bool dsvFuckedUp = false; // In case DSV is _really_ late filling in the menus
            while(i <= nextDays && !dsvFuckedUp) {
                try {
                    DailyMenu menu = await GetRestoMenu(date);
                    if (menu != null) {
                        menus.Add(menu);
                        i++;
                    }
                } catch(DataSourceException ex) {
                    dsvFuckedUp = true;
                }

                date = date.AddDays(1);
            }

            return menus;
        }

        public async Task<SandwichMenu> GetRestoSandwichMenu() {
            if (sandwichMenu.Sandwiches == null) {
                sandwichMenu.Sandwiches = await Get<Sandwich[]>("/sandwiches.json");
            }

            return sandwichMenu;
        }
    }
}
