using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Resto {

    [DataContract]
    public class DailyMenu : IModel {

        public DateTime date { get; set; }
        public string FormattedDate {
            get {
                return date.ToString("dd/M");
            }
        }

        [DataMember(Name = "meat")]
        public Meal[] Meat { get; set; }

        [DataMember(Name = "open")]
        public bool Open { get; set; }

        [DataMember(Name = "soup")]
        public Meal Soup { get; set; }

        [DataMember(Name = "vegetables")]
        public string[] Vegetables { get; set; }
    }
}
