using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hydra.Models.Resto;
using System.Globalization;
using System.Runtime.Serialization.Json;

namespace Hydra.DataSources {
    public class ZeusRestoApi : RestApi, IRestoSource {

        private const string BASE_URL = "https://zeus.ugent.be";
        private const string API_PATH = "/hydra/api/1.0/resto";

        private RestoLocation[] restoLocations;
        private RestoLegendItem[] restoLegendItems;
        private List<DailyMenu> restoMenus;
        private SandwichMenu sandwichMenu;


        public ZeusRestoApi() : base(BASE_URL, API_PATH) {
            sandwichMenu = new SandwichMenu() {
                Sandwiches = null
            };
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

        public async Task<IEnumerable<DailyMenu>> GetRestoMenusThisWeek() {
            if(restoMenus != null) {
                return restoMenus;
            }

            string weekMenuApiUrl = $"/menu/{DateTime.Now.Year}/{getWeekNr()}.json";
            Dictionary<string, DailyMenu> weekMenu = await Get<Dictionary<string, DailyMenu>>(weekMenuApiUrl);
            restoMenus = weekMenuToList(weekMenu);

            return restoMenus;
        }

        public Task<IEnumerable<DailyMenu>> GetRestoMenus(int nextDays) {
            throw new NotImplementedException();
        }

        private int getWeekNr() {
            Calendar calendar = DateTimeFormatInfo.CurrentInfo.Calendar;

            int currentWeekNr = calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            DayOfWeek currentDayNr = calendar.GetDayOfWeek(DateTime.Now);

            // If it's the weekend, we prooooobably want the menus for the following week.
            if (currentDayNr == DayOfWeek.Saturday || currentDayNr == DayOfWeek.Sunday) {
                // Not just +1, in case someone is crazy enough to be looking up the resto menu on New Year's Eve.
                currentWeekNr = calendar.GetWeekOfYear(DateTime.Now.AddDays(7), CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            }

            return currentWeekNr;
        }

        private List<DailyMenu> weekMenuToList(Dictionary<string, DailyMenu> weekMenu) {
            List<DailyMenu> menus = new List<DailyMenu>(5);

            // I hate the current API
            foreach (var keyValue in weekMenu.ToArray()) {
                DateTime date;
                DateTime.TryParse(keyValue.Key, out date);
                keyValue.Value.Date = date;
                menus.Add(keyValue.Value);
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
