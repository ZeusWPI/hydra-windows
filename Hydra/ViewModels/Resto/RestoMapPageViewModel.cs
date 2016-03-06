using Hydra.DataSources;
using Hydra.Models.Resto;
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

namespace Hydra.ViewModels.Resto {
    public class RestoMapPageViewModel : ViewModelBase, INotifyPropertyChanged {

        private readonly IRestoSource restoSource;

        public RestoLocation[] RestoLocations { get; set; }

        public RestoMapPageViewModel(IRestoSource restoSource) {
            this.restoSource = restoSource;
            GetRestoMap();
        }

        public async void GetRestoMap() {
            RestoLocations = await restoSource.GetRestoLocations();
            OnPropertyChanged("RestoLocations");
        }
    }
}
