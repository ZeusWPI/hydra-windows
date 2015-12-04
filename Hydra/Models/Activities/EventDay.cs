using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Activities {
    public class EventDay : List<Activity> {
        public DateTime Date { get; set; }
    }
}
