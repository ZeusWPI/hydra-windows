using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.ViewModels.Home {
    public class HomePageViewModel : ViewModelBase {

        public string DisplayText { get; set; }

        public HomePageViewModel() {
            DisplayText = "HALLOOOOOOOOOOOOOOOOOOOOOO!";
        }
    }
}
