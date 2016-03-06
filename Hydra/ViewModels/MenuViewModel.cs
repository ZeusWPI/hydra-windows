using Hydra.ViewModels.Common;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.ViewModels {

    public class MenuViewModel : ViewModelBase {

        private const string baseIconFolder = "ms-appx:///Assets/Icons/";

        private readonly INavigationService navigationService;

        public ObservableCollection<ButtonViewModel> Commands { get; set; }

        public MenuViewModel(INavigationService navigationService, IResourceLoader resourceLoader) {
            this.navigationService = navigationService;

            Commands = new ObservableCollection<ButtonViewModel>() {
                new NavigationButtonViewModel(navigationService) {
                    DisplayName = resourceLoader.GetString("RootMenu_HomePageDisplayName"),
                    IconSource = baseIconFolder + "Menu-HomeIcon.png",
                    PageToken = PageTokens.HomePage
                },
                new NavigationButtonViewModel(navigationService) {
                    DisplayName = resourceLoader.GetString("RootMenu_RestoPageDisplayName"),
                    IconSource = baseIconFolder + "Menu-RestoIcon.png",
                    PageToken = PageTokens.RestoPage
                },
                new NavigationButtonViewModel(navigationService) {
                    DisplayName = resourceLoader.GetString("RootMenu_InfoPageDisplayName"),
                    IconSource = baseIconFolder + "Menu-InfoIcon.png",
                    PageToken = PageTokens.InfoPage
                },
                new NavigationButtonViewModel(navigationService) {
                    DisplayName = resourceLoader.GetString("RootMenu_NewsPageDisplayName"),
                    IconSource = baseIconFolder + "Menu-NewsIcon.png",
                    PageToken = PageTokens.NewsPage
                },
                new NavigationButtonViewModel(navigationService) {
                    DisplayName = resourceLoader.GetString("RootMenu_ActivitiesPageDisplayName"),
                    IconSource = baseIconFolder + "Menu-ActivitiesIcon.png",
                    PageToken = PageTokens.ActivitiesPage
                },
                new NavigationButtonViewModel(navigationService) {
                    DisplayName = resourceLoader.GetString("RootMenu_SchamperPageDisplayName"),
                    IconSource = baseIconFolder + "Menu-SchamperIcon.png",
                    PageToken = PageTokens.SchamperPage
                },
                new NavigationButtonViewModel(navigationService) {
                    DisplayName = resourceLoader.GetString("RootMenu_UrgentFmPageDisplayName"),
                    IconSource = baseIconFolder + "Menu-UrgentFmIcon.png",
                    PageToken = PageTokens.UrgentFmPage
                },
                new NavigationButtonViewModel(navigationService) {
                    DisplayName = resourceLoader.GetString("RootMenu_SettingsPageDisplayName"),
                    IconSource = baseIconFolder + "Menu-SettingsIcon.png",
                    PageToken = PageTokens.SettingsPage
                }
            };
        }
    }
}
