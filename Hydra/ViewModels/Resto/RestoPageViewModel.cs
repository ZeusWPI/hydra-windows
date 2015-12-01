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
    public class RestoPageViewModel : ViewModelBase, INotifyPropertyChanged {

        private readonly IRestoSource restoSource;

        public ObservableCollection<DailyMenu> RestoInfoList { get; set; }

        public RestoPageViewModel(IRestoSource restoSource) {
            Debug.WriteLine("Derp.");
            this.restoSource = restoSource;
            RestoInfoList = new ObservableCollection<DailyMenu>();

            GetRestoMenus();
        }

        public async void GetRestoMenus() {
            IEnumerable<DailyMenu> restoMenus = await restoSource.GetRestoMenusThisWeek();
            foreach (DailyMenu menu in restoMenus) RestoInfoList.Add(menu);
            OnPropertyChanged();
        }
    }
}
