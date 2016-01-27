using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Schamper {

    public class SchamperDaily {
        
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Summary { get; set; }

        public Uri Link { get; set; }
    }
}
