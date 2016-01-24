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
        /// Returns a RestoMap, containing all resto locations.
        /// </summary>
        /// <returns></returns>
        Task<RestoMap> GetRestoMap();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ICollection<RestoLegendItem>> GetRestoLegendItems();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ICollection<DailyMenu>> GetRestoMenusThisWeek();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ICollection<DailyMenu>> GetRestoMenus(int nextDays);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<SandwichMenu> GetRestoSandwichMenu();
    }
}
