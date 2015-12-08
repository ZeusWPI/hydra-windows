using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Html;

namespace Hydra.Models.News {
    [DataContract]
    public class NewsArticle {

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "content")]
        public string contentHTML { get; set; }
        [IgnoreDataMember]
        public string Content { get { return HtmlUtilities.ConvertToText(contentHTML); } }

        [DataMember(Name = "date")]
        public string DateFormatted { get; set; }
        [IgnoreDataMember]
        public DateTime Date {
            get { return DateTime.Parse(DateFormatted, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal); }
            set { DateFormatted = value.ToString("O"); }
        }

        [DataMember(Name = "association")]
        public Dictionary<string, string> Association { get; set; }

        [DataMember(Name = "hightlighted")]
        public int Highlighted { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [IgnoreDataMember]
        public string Link { get { return "http://student.ugent.be/nieuws/nieuws.php?ID=" + Id; } }
    }
}
