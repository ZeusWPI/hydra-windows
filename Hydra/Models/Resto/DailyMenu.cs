using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Resto {

    [DataContract]
    public class DailyMenu : IModel {

        public DateTime Date { get; set; }

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
