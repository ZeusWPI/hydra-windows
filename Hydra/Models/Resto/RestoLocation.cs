using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Hydra.Models.Resto {

    [DataContract]
    public class RestoLocation : IModel {
        public static readonly string[] types = new string[] {
            "resto",
            "cafetaria",
            "club"
        };


        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }

        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }

        public Geopoint Location {
            get {
                BasicGeoposition pos = new BasicGeoposition() {
                    Latitude = Latitude,
                    Longitude = Longitude
                };
                return new Geopoint(pos);
            }
        }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

}
