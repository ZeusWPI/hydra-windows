using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Hydra.Models.Resto {

    public class RestoLocation : IModel {
        public static readonly string[] types = new string[] {
            "resto",
            "cafetaria",
            "club"
        };

        public string name { get; set; }
        public string address { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public Geopoint Location {
            get {
                BasicGeoposition pos = new BasicGeoposition {
                    Latitude = latitude,
                    Longitude = longitude
                };
                return new Geopoint(pos);
            }
        }
        public string type { get; set; }
    }

}
