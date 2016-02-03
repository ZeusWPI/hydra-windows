using Hydra.DataSources;
using Hydra.Models.News;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Hydra.ViewModels.News {
    public class NewsPageViewModel : ViewModelBase, INotifyPropertyChanged {

        private readonly INewsSource newsSource;

        public ObservableCollection<NewsArticle> NewsArticles { get; set; }

        public NewsPageViewModel(INewsSource newsSource) {
            this.newsSource = newsSource;
            NewsArticles = new ObservableCollection<NewsArticle>();

            var newsArticlesTask = GetNewsArticles();
        }

        public async Task GetNewsArticles() {
            IEnumerable<NewsArticle> articles = await newsSource.GetArticles(DateTime.Today.AddYears(-1));
            foreach (NewsArticle article in articles) NewsArticles.Add(article);
            OnPropertyChanged();
        }
    }
}
