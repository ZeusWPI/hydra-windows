using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hydra.Models.Resto;
using System.Globalization;
using System.Runtime.Serialization.Json;
using Hydra.Models.Activities;
using Hydra.Models.News;

namespace Hydra.DataSources {

    /// <summary>
    /// The DSA REST API provides all news articles, activities and associations at Ghent University.
    /// </summary>
    public class DsaApi : RestApi, IAssociationSource, IActivitySource, INewsSource {

        private const string BASE_URL = "http://student.ugent.be/";
        private const string API_PATH = "/hydra/api/1.0/";

        private Association[] associations;
        private Activity[] activities;
        private NewsArticle[] newsArticles;

        public DsaApi() : base(BASE_URL, API_PATH) {
        }
        
        public async Task<IEnumerable<Association>> GetAssociations() {
            if(associations == null) {
                associations = await Get<Association[]>("associations.json");
            }
            
            return associations;
        }

        public async Task<Association> GetAssociation(string internalName) {
            return (await GetAssociations()).Single(assocation => assocation.InternalName.Equals(internalName, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<Konvent>> GetAssociationsByKonvent() {
            List<Konvent> konvents = new List<Konvent>();

            var query = from association in await GetAssociations()
                        group association by association.ParentAssociation into grp
                        orderby grp.Key
                        select new { GroupName = grp.Key, Items = grp };

            foreach (var grp in query) {
                Konvent konvent = new Konvent();
                konvent.KonventName = grp.GroupName;
                foreach (var item in grp.Items) {
                    konvent.Add(item);
                }
                konvents.Add(konvent);
            }

            return konvents;
        }

        public async Task<IEnumerable<Activity>> GetActivities() {
            if (activities == null) {
                activities = await Get<Activity[]>("all_activities.json");

                foreach(Activity activity in activities) {
                    activity.Association = await GetAssociation(activity.associationLinks["internal_name"]);
                }
            }

            return activities;
        }

        public async Task<IEnumerable<EventDay>> GetActivitiesByDate() {
            List<EventDay> dates = new List<EventDay>();

            var query = from activity in await GetActivities()
                        group activity by activity.Start.Date into grp
                        orderby grp.Key
                        select new { GroupName = grp.Key, Items = grp };

            foreach (var grp in query) {
                EventDay eventDay = new EventDay();
                eventDay.Date = grp.GroupName;
                foreach (var item in grp.Items) {
                    eventDay.Add(item);
                }
                dates.Add(eventDay);
            }

            return dates;
        }

        public async Task<IEnumerable<NewsArticle>> GetArticles() {
            if (newsArticles == null) {
                newsArticles = await Get<NewsArticle[]>("all_news.json");
            }

            return newsArticles;
        }

    }
}
