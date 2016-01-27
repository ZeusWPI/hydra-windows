using Hydra.Models.Schamper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace Hydra.DataSources {
    public class SchamperDailyFeed : RssFeed {

        private static readonly string DAILIES_URL = "http://www.schamper.ugent.be/dagelijks";

        public SchamperDailyFeed() : base(DAILIES_URL) {
        }

        public async Task<SchamperDaily[]> GetDailies() {
            IList<SyndicationItem> feedItems = (await GetFeed()).Items;

            SchamperDaily[] dailies = new SchamperDaily[feedItems.Count];
            int i = 0;
            foreach(SyndicationItem item in feedItems) {
                dailies[i] = new SchamperDaily() {
                    Title = item.Title.Text,
                    Date = item.PublishedDate.DateTime,
                    Summary = item.Summary.Text
                };
                i++;
            }

            return dailies;
        }
    }
}
