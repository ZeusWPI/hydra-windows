using Hydra.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace Hydra.DataSources {
    /// <summary>
    /// Represents a RSS-feed
    /// </summary>
    public class RssFeed : IDataSource {

        public Uri FeedUrl { get; private set; }
        private SyndicationClient syndicationClient;

        private SyndicationFeed feed;

        public RssFeed(string feedUrl) {
            this.FeedUrl = new Uri(feedUrl);
            this.syndicationClient = new SyndicationClient();
            this.feed = null;

            var feedPromise = GetFeed();
        }

        public async Task<SyndicationFeed> GetFeed() {
            if(this.feed == null) {
                try {
                    this.feed = await syndicationClient.RetrieveFeedAsync(this.FeedUrl);
                } catch (Exception ex) {
                    throw new DataSourceException("Couldn't connect to the server with url " + this.FeedUrl, ex);
                }
            }

            return this.feed;
        }
    }
}
