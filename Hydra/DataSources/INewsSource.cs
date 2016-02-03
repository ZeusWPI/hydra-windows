using Hydra.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.DataSources {

    /// <summary>
    /// Provides the data for the activities.
    /// </summary>
    public interface INewsSource {

        /// <summary>
        /// Returns the articles of the source.
        /// </summary>
        /// <returns>A collection of articles</returns>
        Task<IEnumerable<NewsArticle>> GetArticles(bool sorted = true);

        /// <summary>
        /// Returns the articles of the source, starting from the given date.
        /// </summary>
        /// <returns>A collection of articles</returns>
        Task<IEnumerable<NewsArticle>> GetArticles(DateTime startDate, bool sorted = true);

    }
}
