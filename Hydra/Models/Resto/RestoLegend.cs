using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Resto {

    public class RestoLegend : IModel {
        public static readonly string[] styles = new string[] {
            "bold"
        };

        [DataMember(Name = "key")]
        public string key { get; set; }

        [DataMember(Name = "style")]
        public string style { get; set; }

        [DataMember(Name = "value")]
        public string value { get; set; }
    }

}
