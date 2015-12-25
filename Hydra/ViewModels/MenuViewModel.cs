using Prism.Commands;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Hydra.ViewModels {

    public class MenuViewModel : ViewModelBase {

        private const string baseIconFolder = "ms-appx:///Assets/Icons/";

        private readonly INavigationService navigationService;

        public ObservableCollection<MenuItemViewModel> Commands { get; set; }

        public MenuViewModel(INavigationService navigationService, IResourceLoader resourceLoader) {
            this.navigationService = navigationService;

            Commands = new ObservableCollection<MenuItemViewModel>() {
                new MenuItemViewModel {
                    DisplayName = resourceLoader.GetString("RootMenu_HomePageDisplayName"),
                    IconSource = baseIconFolder + "Menu-HomeIcon.png",
                    Command = new DelegateCommand(NavigateToPage(PageTokens.HomePage))
                },
                new MenuItemViewModel {
                    DisplayName = resourceLoader.GetString("RootMenu_RestoPageDisplayName"),
                    IconSource = baseIconFolder + "Menu-RestoIcon.png",
                    Command = new DelegateCommand(NavigateToPage(PageTokens.RestoPage))
                },
                new MenuItemViewModel {
                    DisplayName = resourceLoader.GetString("RootMenu_InfoPageDisplayName"),
                    IconSource = baseIconFolder + "Menu-InfoIcon.png",
                    Command = new DelegateCommand(NavigateToPage(PageTokens.NewsPage))
                },
                new MenuItemViewModel {
                    DisplayName = resourceLoader.GetString("RootMenu_NewsPageDisplayName"),
                    IconSource = baseIconFolder + "Menu-NewsIcon.png",
                    Command = new DelegateCommand(NavigateToPage(PageTokens.NewsPage))
                },
                new MenuItemViewModel {
                    DisplayName = resourceLoader.GetString("RootMenu_ActivitiesPageDisplayName"),
                    IconSource = baseIconFolder + "Menu-ActivitiesIcon.png",
                    Command = new DelegateCommand(NavigateToPage(PageTokens.ActivitiesPage))
                }
            };
        }

        private Action NavigateToPage(string page) {
            return () => { this.navigationService.Navigate(page, null); };
        }
    }
}
