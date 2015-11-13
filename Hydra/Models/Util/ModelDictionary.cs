using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Util {
    public class ModelDictionary {
        public Dictionary<string, object> dict;

        public ModelDictionary() {
            dict = new Dictionary<string, object>();
        }
    }
}
