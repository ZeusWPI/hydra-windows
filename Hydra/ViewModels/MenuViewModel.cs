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
using Windows.UI.Xaml.Controls;

namespace Hydra.ViewModels {

    public class MenuViewModel : ViewModelBase {

        private readonly INavigationService navigationService;

        public ObservableCollection<MenuItemViewModel> Commands { get; set; }

        public MenuViewModel(INavigationService navigationService, IResourceLoader resourceLoader) {
            this.navigationService = navigationService;

            Commands = new ObservableCollection<MenuItemViewModel>() {
                new MenuItemViewModel {
                    DisplayName = resourceLoader.GetString("RootMenu_HomePageDisplayName"),
                    SymbolIcon = Symbol.Home,
                    Command = new DelegateCommand(NavigateToPage(PageTokens.HomePage))
                },
                new MenuItemViewModel {
                    DisplayName = resourceLoader.GetString("RootMenu_RestoPageDisplayName"),
                    SymbolIcon = Symbol.Favorite,
                    Command = new DelegateCommand(NavigateToPage(PageTokens.RestoPage))
                },
                new MenuItemViewModel {
                    DisplayName = resourceLoader.GetString("RootMenu_InfoPageDisplayName"),
                    SymbolIcon = Symbol.Help,
                    Command = new DelegateCommand(NavigateToPage(PageTokens.RestoPage))
                },
                new MenuItemViewModel {
                    DisplayName = resourceLoader.GetString("RootMenu_ActivitiesPageDisplayName"),
                    SymbolIcon = Symbol.Calendar,
                    Command = new DelegateCommand(NavigateToPage(PageTokens.ActivitiesPage))
                }
            };
        }

        private Action NavigateToPage(string page) {
            return () => { this.navigationService.Navigate(page, null); };
        }
    }
}
