using Hydra.Models.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.DataSources {

    /// <summary>
    /// Provides the data for the activities.
    /// </summary>
    public interface IActivitySource {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Activity>> GetActivities();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EventDay>> GetActivitiesByDate();
    }
}
