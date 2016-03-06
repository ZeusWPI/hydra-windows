using Hydra.DataSources;
using Hydra.Exceptions;
using Hydra.Models.Schamper;
using Hydra.ViewModels.Common;
using Hydra.Views.Common;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.ViewModels.Schamper {
    public class SchamperPageViewModel : AbstractPageViewModel, INotifyPropertyChanged {

        private readonly SchamperDailyFeed schamperDailyFeed;

        public SchamperDaily[] Dailies { get; set; }

        public SchamperPageViewModel(IResourceLoader resourceLoader, SchamperDailyFeed schamperDailyFeed) : base(resourceLoader) {
            this.schamperDailyFeed = schamperDailyFeed;
            GetDailies();
        }

        public async void GetDailies() {
            try {
                Dailies = await schamperDailyFeed.GetDailies();
                OnPropertyChanged();
            } catch (DataSourceException ex) {
                await ErrorDialogFactory.NetworkErrorDialog().ShowAsync();
            }
        }
    }
}
