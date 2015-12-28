using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Resto {
    [DataContract]
    public class Sandwich {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "ingredients")]
        public string Ingredients { get; set; }

        [DataMember(Name = "price_small")]
        public string PriceSmall { get; set; }

        [DataMember(Name = "price_medium")]
        public string PriceMedium { get; set; }
    }
}
