using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Hydra.Models.Activities {

    [DataContract]
    public class Activity {

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "start")]
        private string startFormatted { get; set; }
        [IgnoreDataMember]
        public DateTime Start {
            get { return DateTime.Parse(startFormatted, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal); }
            set { startFormatted = value.ToString("O"); }
        }

        [DataMember(Name = "end")]
        public string endFormatted { get; set; }
        [IgnoreDataMember]
        public DateTime End {
            get { return DateTime.Parse(endFormatted, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal); }
            set { endFormatted = value.ToString("O"); }
        }

        [DataMember(Name = "location")]
        public string Location { get; set; }

        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }

        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [IgnoreDataMember]
        public string Thumbnail { get { return Association.Logo; } }

        [DataMember(Name = "url")]
        public Uri Url { get; set; }

        [DataMember(Name = "facebook_id")]
        public string FacebookId { get; set; }

        [DataMember(Name = "categories")]
        public string[] Categories { get; set; }

        [DataMember(Name = "highlighted")]
        public int Highlighted { get; set; }

        // WHY THE HELL DOESN'T THIS FOLLOW THE OTHER ASSOCIATION FORMAT
        [DataMember(Name = "association")]
        public Dictionary<string, string> associationLinks { get; set; }

        [IgnoreDataMember]
        public Association Association { get; set; }
    }
}
