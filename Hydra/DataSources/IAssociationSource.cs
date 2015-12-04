using Hydra.Models.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.DataSources {

    /// <summary>
    /// Provides the data for the restos.
    /// </summary>
    public interface IAssociationSource {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Association>> GetAssociations();
    }
}
