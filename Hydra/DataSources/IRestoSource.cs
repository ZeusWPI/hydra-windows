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
        /// Returns the resto menu for the next few days.
        /// </summary>
        /// <param name="nextDays">The amount of days (excluding today)
        /// to fetch the resto menu from</param>
        /// <returns></returns>
        Task<ICollection<DailyMenu>> GetRestoMenus(int nextDays = 1);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<SandwichMenu> GetRestoSandwichMenu();
    }
}
