using Hydra.DataSources;
using Hydra.Exceptions;
using Hydra.Models.News;
using Hydra.ViewModels.Common;
using Hydra.Views.Common;
using Prism.Windows.AppModel;
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
    public class NewsPageViewModel : AbstractPageViewModel, INotifyPropertyChanged {

        private readonly INewsSource newsSource;

        public ObservableCollection<NewsArticle> NewsArticles { get; set; }

        public NewsPageViewModel(IResourceLoader resourceLoader, INewsSource newsSource) : base(resourceLoader) {
            this.newsSource = newsSource;
            NewsArticles = new ObservableCollection<NewsArticle>();

            GetNewsArticles();
        }

        public async void GetNewsArticles() {
            try {
                IEnumerable<NewsArticle> articles = await newsSource.GetArticles(DateTime.Today.AddYears(-1));
                foreach (NewsArticle article in articles) NewsArticles.Add(article);
                OnPropertyChanged();
            } catch (DataSourceException ex) {
                await ErrorDialogFactory.NetworkErrorDialog().ShowAsync();
            }
        }
    }
}
