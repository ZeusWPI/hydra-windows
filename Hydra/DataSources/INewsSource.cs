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
        /// <returns></returns>
        Task<IEnumerable<NewsArticle>> GetArticles();
        
    }
}
