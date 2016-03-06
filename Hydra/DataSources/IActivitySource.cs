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
        /// Returns all activities at UGent it knows of.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Activity>> GetActivities();

        /// <summary>
        /// Returns all activities at UGent it knows of in a given date
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Activity>> GetActivities(DateTime date);

        /// <summary>
        /// Returns all activities at UGent it knows of in a given span of days
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Activity>> GetActivities(DateTime from, DateTime to);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EventDay>> GetActivitiesByDate();
    }
}
