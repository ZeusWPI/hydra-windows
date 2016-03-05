using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Models.Resto {

    [DataContract]
    public class DailyMenu : IModel {

        [DataMember(Name = "date")]
        private string dateFormatted { get; set; }
        public DateTime Date {
            get { return DateTime.Parse(dateFormatted); }
            set { dateFormatted = value.ToString("yyyy-MM-dd"); }
        }

        [DataMember(Name = "meals")]
        public Meal[] Meals { get; set; }

        [IgnoreDataMember]
        public IEnumerable<Meal> MainDishes {
            get { return Meals.Where(meal => meal.Type == "main"); }
        }

        [IgnoreDataMember]
        public IEnumerable<Meal> SideDishes {
            get { return Meals.Where(meal => meal.Type == "side"); }
        }

        [DataMember(Name = "open")]
        public bool Open { get; set; }

        [DataMember(Name = "vegetables")]
        public string[] Vegetables { get; set; }
    }
}
