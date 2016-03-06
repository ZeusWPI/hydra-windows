using Hydra.DataSources;
using Hydra.Models.Resto;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.ViewModels.Home {
    public class HomePageViewModel : ViewModelBase {

        private readonly IRestoSource restoSource;
        public DailyMenu RestoMenuToday { get; set; }

        public string WelcomeTitle { get; set; }
        public string WelcomeText { get; set; }

        public HomePageViewModel(IRestoSource restoSource) {
            this.restoSource = restoSource;

            this.WelcomeTitle = "Welkom bij Hydra!";
            this.WelcomeText = "Welkom bij Hydra, de app voor alle UGent-studenten.";

            getRestoMenuToday();
        }

        public async void getRestoMenuToday() {
            RestoMenuToday = await restoSource.GetRestoMenu();
            OnPropertyChanged("RestoMenuToday");
        }
    }
}
