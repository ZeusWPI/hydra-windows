﻿using Hydra.Models.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.DataSources {

    /// <summary>
    /// Provides the data for the associations from the DSA.
    /// </summary>
    public interface IAssociationSource {

        /// <summary>
        /// Return all associations from UGent
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Association>> GetAssociations();

        /// <summary>
        /// Return all associations from UGent, grouped by konvent
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Konvent>> GetAssociationsByKonvent();
    }
}
