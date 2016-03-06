using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hydra.Models.Resto;

namespace Hydra.DataSources {

    /// <summary>
    /// Provides the data for the restos.
    /// </summary>
    public interface IRestoSource {

        /// <summary>
        /// Returns a list of all resto locations.
        /// </summary>
        /// <returns></returns>
        Task<RestoLocation[]> GetRestoLocations();

        /// <summary>
        /// Returns today's resto menu, or of the next weekday.
        /// </summary>
        /// <returns>The menu of today or the next weekday.</returns>
        Task<DailyMenu> GetRestoMenu();

        /// <summary>
        /// Returns the resto menu of the given day.
        /// </summary>
        /// <param name="date">The day to get the menu from.</param>
        /// <returns>The menu of the given day, or null if there is none (e.g. the weekend).</returns>
        Task<DailyMenu> GetRestoMenu(DateTime date);

        /// <summary>
        /// Returns the resto menu for the next few days, excluding the days on which
        /// there certainly is no menu.
        /// </summary>
        /// <param name="nextDays">The amount of days (excluding today)
        /// to fetch the resto menu from</param>
        /// <returns></returns>
        Task<ICollection<DailyMenu>> GetRestoMenus(int nextDays = 1);

        /// <summary>
        /// Returns the sandwich menu.
        /// </summary>
        /// <returns>The sandwich menu</returns>
        Task<SandwichMenu> GetRestoSandwichMenu();
    }
}
