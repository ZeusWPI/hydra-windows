using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Resto {
    public class RestoMeta : IModel {
        public RestoLegend[] legend { get; set; }
        public RestoLocation[] locations { get; set; }
    }

}
