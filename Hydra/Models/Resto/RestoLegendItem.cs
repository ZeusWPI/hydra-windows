using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Resto {

    [DataContract]
    public class RestoLegendItem : IModel {
        public static readonly string[] styles = new string[] {
            "bold"
        };

        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "style")]
        public string Style { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        public override string ToString() {
            return Key + ": " + Value;
        }
    }

}
