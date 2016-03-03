using Hydra.DataSources;
using Hydra.Exceptions;
using Hydra.Models.Resto;
using Hydra.ViewModels.Util;
using Hydra.Views.Common;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
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

        public ObservableCollection<object> RestoInfoList { get; set; }
        public ObservableCollection<RestoLegendItem> Legend { get; set; }

        public ButtonViewModel MapButton { get; set; }

        public RestoPageViewModel(IRestoSource restoSource, INavigationService navigationService) {
            this.restoSource = restoSource;
            this.MapButton = new NavigationButtonViewModel(navigationService) {
                IconSource = "ms-appx:///Assets/Icons/Resto-MapIcon.png",
                DisplayName = "Kaart",
                PageToken = PageTokens.RestoMapPage
            };

            RestoInfoList = new ObservableCollection<object>();
            Legend = new ObservableCollection<RestoLegendItem>();
            var restoMenusTask = GetRestoMenus();
            var restoLegendTask = GetRestoLegend();
            var restoSandwichMenuTask = GetRestoSandwichMenu();
        }

        public async Task GetRestoMenus() {
            try {
                IEnumerable<DailyMenu> restoMenus = await restoSource.GetRestoMenusThisWeek();
                foreach (var restoMenu in restoMenus) {
                    RestoInfoList.Add(restoMenu);
                }
                OnPropertyChanged();
            } catch(DataSourceException ex) {
                await ErrorDialogFactory.NetworkErrorDialog().ShowAsync();
            }
        }

        public async Task GetRestoLegend() {
            try {
                RestoLegendItem[] legendItems = await restoSource.GetRestoLegendItems();
                foreach (RestoLegendItem legendItem in legendItems) Legend.Add(legendItem);
                OnPropertyChanged();
            } catch (DataSourceException ex) {
                await ErrorDialogFactory.NetworkErrorDialog().ShowAsync();
            }
        }

        public async Task GetRestoSandwichMenu() {
            try {
                RestoInfoList.Add(await restoSource.GetRestoSandwichMenu());
                OnPropertyChanged();
            } catch (DataSourceException ex) {
                await ErrorDialogFactory.NetworkErrorDialog().ShowAsync();
            }
        }
    }
}
