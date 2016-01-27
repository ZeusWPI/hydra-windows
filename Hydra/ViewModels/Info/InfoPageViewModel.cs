using Hydra.ViewModels.Util;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.ViewModels.Info {
    public class InfoPageViewModel : ViewModelBase {

        private readonly string icons_basefolder = "ms-appx:///Assets/Icons/";

        private readonly INavigationService navigationService;

        public ObservableCollection<InfoPageItemViewModel> InfoItems { get; set; }

        public InfoPageViewModel(INavigationService navigationService) {
            this.navigationService = navigationService;

            InfoItems = new ObservableCollection<InfoPageItemViewModel>() {
                new InfoPageItemViewModel() {
                    Title = "Studies",
                    Links = new ObservableCollection<ButtonViewModel>() {
                        new NavigationButtonViewModel(navigationService) {
                            IconSource = icons_basefolder + "Info-MinervaIcon.png",
                            DisplayName = "Minerva",
                            PageToken = PageTokens.MinervaPage
                        },
                        new NavigationButtonViewModel(navigationService) {
                            IconSource = icons_basefolder + "Info-OasisIcon.png",
                            DisplayName = "Oasis",
                            PageToken = PageTokens.OasisPage
                        },
                        new NavigationButtonViewModel(navigationService) {
                            IconSource = icons_basefolder + "Info-EpurseIcon.png",
                            DisplayName = "Epurse",
                            PageToken = PageTokens.EpursePage
                        },
                        new NavigationButtonViewModel(navigationService) {
                            IconSource = icons_basefolder + "Info-EduroamIcon.png",
                            DisplayName = "Eduroam",
                            PageToken = PageTokens.EduroamPage
                        },
                        new NavigationButtonViewModel(navigationService) {
                            IconSource = icons_basefolder + "Info-VpnIcon.png",
                            DisplayName = "VPN",
                            PageToken = PageTokens.VpnPage
                        },
                        new NavigationButtonViewModel(navigationService) {
                            IconSource = icons_basefolder + "Info-CalendarIcon.png",
                            DisplayName = "Academische kalender",
                            PageToken = PageTokens.AcademicCalendarPage
                        }
                    }
                },
                new InfoPageItemViewModel() {
                    Title = "Voorzieningen",
                    Links = new ObservableCollection<ButtonViewModel>() {
                        new NavigationButtonViewModel(navigationService) {
                            IconSource = icons_basefolder + "Info-DoctorIcon.png",
                            DisplayName = "Studentenarts",
                            PageToken = PageTokens.DoctorPage
                        },
                        new NavigationButtonViewModel(navigationService) {
                            IconSource = icons_basefolder + "Info-BicycleIcon.png",
                            DisplayName = "StudentenENMobiliteit",
                            PageToken = PageTokens.BicyclePage
                        },
                        new NavigationButtonViewModel(navigationService) {
                            IconSource = icons_basefolder + "Info-StudyLocationIcon.png",
                            DisplayName = "Bloklocaties",
                            PageToken = PageTokens.BlokmapPage
                        }
                    }
                }
            };
        }
    }
}
