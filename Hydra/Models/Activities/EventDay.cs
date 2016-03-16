using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Activities {
    public class EventDay {
        public DateTime Date { get; set; }

        public List<Activity> Activities { get; set; }
    }
}
