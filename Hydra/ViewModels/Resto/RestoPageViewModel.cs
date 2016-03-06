using Hydra.DataSources;
using Hydra.Exceptions;
using Hydra.Models.Resto;
using Hydra.ViewModels.Common;
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

namespace Hydra.ViewModels.Resto {
    public class RestoPageViewModel : ViewModelBase, INotifyPropertyChanged {

        private readonly IRestoSource restoSource;

        public ObservableCollection<RestoMenu> RestoInfoList { get; set; }

        public ButtonViewModel MapButton { get; set; }

        public RestoPageViewModel(IRestoSource restoSource, INavigationService navigationService) {
            this.restoSource = restoSource;
            this.MapButton = new NavigationButtonViewModel(navigationService) {
                IconSource = "ms-appx:///Assets/Icons/Resto-MapIcon.png",
                DisplayName = "Kaart",
                PageToken = PageTokens.RestoMapPage
            };

            this.RestoInfoList = new ObservableCollection<RestoMenu>();
            GetRestoMenus();
            GetRestoSandwichMenu();
        }

        public async void GetRestoMenus() {
            try {
                IEnumerable<DailyMenu> restoMenus = await restoSource.GetRestoMenus(4);
                foreach (var restoMenu in restoMenus) {
                    RestoInfoList.Add(restoMenu);
                }
                OnPropertyChanged();
            } catch(DataSourceException ex) {
                await ErrorDialogFactory.NetworkErrorDialog().ShowAsync();
            }
        }

        public async void GetRestoSandwichMenu() {
            try {
                RestoInfoList.Add(await restoSource.GetRestoSandwichMenu());
                OnPropertyChanged();
            } catch (DataSourceException ex) {
                await ErrorDialogFactory.NetworkErrorDialog().ShowAsync();
            }
        }
    }
}
