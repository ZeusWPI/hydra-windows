using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
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
        /// <param name="nextDays">The amount of days to download the info from.</param>
        public async Task<DailyMenu[]> GetRestoMenus() {
            string weekMenuApiUrl = "week/" + getWeekNr() + ".json";
            HttpContent jsonFile = await downloadJsonFile(restoApiUrl, weekMenuApiUrl);
            string jsonString = await jsonFile.ReadAsStringAsync();
            return parseWeekMenu(jsonString);
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


        private DailyMenu[] parseWeekMenu(string jsonString) {
            List<DailyMenu> menus = new List<DailyMenu>(5);
            JsonObject jsonObject = JsonObject.Parse(jsonString);

            foreach (string day in jsonObject.Keys) {
                var dailyMenu = jsonObject.GetNamedObject(day);
                DailyMenu menu = new DailyMenu() {
                    open = dailyMenu.GetNamedBoolean("open")
                };

                if (menu.open) {
                    // Date
                    DateTime date;
                    DateTime.TryParse(day, out date);
                    menu.date = date;

                    // Meat
                    var meat = dailyMenu.GetNamedArray("meat");
                    List<Meal> meals = new List<Meal>();
                    foreach (var el in meat) {
                        var meal = el.GetObject();
                        meals.Add(new Meal() {
                            name = meal.GetNamedString("name"),
                            price = meal.GetNamedString("price"),
                            recommended = meal.GetNamedBoolean("recommended")
                        });
                    }
                    menu.meat = meals.ToArray();

                    // Soup
                    var soupObject = dailyMenu.GetNamedObject("soup");
                    menu.soup = new Meal() {
                        name = soupObject.GetNamedString("name"),
                        price = soupObject.GetNamedString("price")
                    };

                    // Vegetables
                    var vegetables = dailyMenu.GetNamedArray("vegetables");
                    List<string> vegetableList = new List<string>();
                    foreach (var el in vegetables) {
                        vegetableList.Add(el.GetString());
                    }
                    menu.vegetables = vegetableList.ToArray();

                    menus.Add(menu);
                }
            }

            return menus.ToArray();
        }
    }
}
