using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hydra.Models.Resto;
using System.Globalization;
using System.Runtime.Serialization.Json;
using Hydra.Models.Activities;

namespace Hydra.DataSources {
    public class DsaAssociationsApi : RestApi, IAssociationSource {

        private const string BASE_URL = "http://student.ugent.be/";
        private const string API_PATH = "/hydra/api/1.0/associations.json";

        private Association[] associations;


        public DsaAssociationsApi() : base(BASE_URL, API_PATH) {
        }

        /// <summary>
        /// Retrieves the locations of all resto's.
        /// </summary>
        public async Task<IEnumerable<Association>> GetAssociations() {
            if(associations == null) {
                associations = await Get<Association[]>("");
            }
            
            return associations;
        }
        
    }
}
