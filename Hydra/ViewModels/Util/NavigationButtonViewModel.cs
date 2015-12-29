using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hydra.ViewModels.Util {

    /// <summary>
    /// A default viewmodel for a button
    /// </summary>
    public class NavigationButtonViewModel : ButtonViewModel {
        private readonly INavigationService navigationService;

        private string navigationParameter = null;
        public string NavigationParameter {
            get { return navigationParameter; }
            set {
                navigationParameter = value;
                setCommand();
            }
        }

        private string pageToken;
        public string PageToken {
            get { return pageToken; }
            set {
                pageToken = value;
                setCommand();
            }
        }

        public NavigationButtonViewModel(INavigationService navigationService) {
            this.navigationService = navigationService;
        }

        private void setCommand() {
            this.Command = new DelegateCommand(() => { this.navigationService.Navigate(this.PageToken, NavigationParameter); });
        }
    }
}
