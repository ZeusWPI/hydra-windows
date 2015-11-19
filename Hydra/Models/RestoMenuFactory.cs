using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

using Hydra.Models.Resto;

namespace Hydra.Models {
    public class RestoMenuFactory : ModelFactory {

        private const string restoApiUrl = zeusApiUrl + "1.0/resto/";

        public RestoMenuFactory() {

        }

        /// <summary>
        /// Get the menus for this week (or next week if it's already weekend).
        /// </summary>
        public async Task<DailyMenu[]> GetRestoMenus() {
            string weekMenuApiUrl = "week/" + getWeekNr() + ".json";

            DataContractJsonSerializerSettings serializerSettings = new DataContractJsonSerializerSettings() {
                UseSimpleDictionaryFormat = true
            };

            var weekMenu = await FromRestApi(typeof(Dictionary<string, DailyMenu>), restoApiUrl, weekMenuApiUrl, serializerSettings);
            return weekMenuToArray((Dictionary<string, DailyMenu>) weekMenu);
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

        private DailyMenu[] weekMenuToArray(Dictionary<string, DailyMenu> weekMenu) {
            List<DailyMenu> menus = new List<DailyMenu>(5);
            
            // I hate the current API
            foreach(var keyValue in weekMenu.ToArray()) {
                DateTime date;
                DateTime.TryParse(keyValue.Key, out date);
                keyValue.Value.date = date;
                menus.Add(keyValue.Value);
            }

            return menus.ToArray();
        }
    }
}
