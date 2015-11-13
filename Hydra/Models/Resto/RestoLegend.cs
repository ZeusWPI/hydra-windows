using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Resto {

    public class RestoLegend : IModel {
        public static readonly string[] styles = new string[] {
            "bold"
        };

        public string key { get; set; }
        public string style { get; set; }
        public string value { get; set; }
    }

}
