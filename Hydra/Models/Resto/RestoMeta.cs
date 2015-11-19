using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Resto {

    [DataContract]
    public class RestoMeta : IModel {

        [DataMember(Name = "legend")]
        public RestoLegend[] Legend { get; set; }

        [DataMember(Name = "locations")]
        public RestoLocation[] Locations { get; set; }
    }

}
