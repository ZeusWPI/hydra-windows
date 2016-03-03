using Hydra.DataSources;
using Hydra.Exceptions;
using Hydra.Models.Schamper;
using Hydra.Views.Common;
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
using Windows.Web.Syndication;

namespace Hydra.ViewModels.Schamper {
    public class SchamperPageViewModel : ViewModelBase, INotifyPropertyChanged {

        private readonly SchamperDailyFeed schamperDailyFeed;

        public SchamperDaily[] Dailies { get; set; }

        public SchamperPageViewModel(SchamperDailyFeed schamperDailyFeed) {
            this.schamperDailyFeed = schamperDailyFeed;

            var schamperDailyTask = GetDailies();
        }

        public async Task GetDailies() {
            try {
                Dailies = await schamperDailyFeed.GetDailies();
                OnPropertyChanged();
            } catch (DataSourceException ex) {
                await ErrorDialogFactory.NetworkErrorDialog().ShowAsync();
            }
        }
    }
}
