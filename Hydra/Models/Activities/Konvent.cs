using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Activities {
    public class Konvent : List<Association> {
        public string KonventName { get; set; }
        public string Associations { get; set; }
    }
}
