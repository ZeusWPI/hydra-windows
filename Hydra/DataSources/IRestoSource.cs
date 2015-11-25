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
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<RestoLocation>> GetRestoLocations();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<RestoLegendItem>> GetRestoLegendItems();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DailyMenu>> GetRestoMenusThisWeek();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DailyMenu>> GetRestoMenus(int nextDays);
    }
}
