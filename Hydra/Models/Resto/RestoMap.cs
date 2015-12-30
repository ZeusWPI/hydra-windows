using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Hydra.Models.Resto {
    
    public class RestoMap {

        public Geopoint Center {
            get {
                var ghentCenter = new BasicGeoposition() {
                    Latitude = 51.053458,
                    Longitude = 3.73038
                };

                return new Geopoint(ghentCenter);
            }
        }

        public RestoLocation[] RestoLocations { get; set; }
    }
}
