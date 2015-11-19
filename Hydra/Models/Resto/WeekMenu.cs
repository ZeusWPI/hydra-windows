using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Resto {
    public class WeekMenu : IModel {

        public DateTime date { get; set; }
        public string FormattedDate {
            get {
                return date.ToString("dd/M");
            }
        }
        public Meal[] meat { get; set; }
        public bool open { get; set; }
        public Meal soup { get; set; }
        public string[] vegetables { get; set; }
    }
}
