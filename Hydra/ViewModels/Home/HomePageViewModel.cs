using Hydra.DataSources;
using Hydra.Models.Activities;
using Hydra.Models.Resto;
using Hydra.ViewModels.Common;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.ViewModels.Home {
    public class HomePageViewModel : AbstractPageViewModel {

        private readonly IRestoSource restoSource;
        private readonly IActivitySource activitySource;
        public DailyMenu RestoMenuToday { get; set; }
        public IEnumerable<Activity> ActivitiesToday { get; set; }

        public string WelcomeTitle { get; set; }
        public string WelcomeText { get; set; }

        public HomePageViewModel(IResourceLoader resourceLoader, IRestoSource restoSource, IActivitySource activitySource) : base(resourceLoader) {
            this.restoSource = restoSource;
            this.activitySource = activitySource;

            this.WelcomeTitle = "Welkom bij Hydra!";
            this.WelcomeText = "Welkom bij Hydra, de app voor alle UGent-studenten.";

            getRestoMenuToday();
            getActivitiesToday();
        }

        public async void getRestoMenuToday() {
            RestoMenuToday = await restoSource.GetRestoMenu();
            OnPropertyChanged("RestoMenuToday");
        }

        public async void getActivitiesToday() {
            ActivitiesToday = await activitySource.GetActivities(DateTime.Now,  DateTime.Now.AddDays(1));
            OnPropertyChanged("ActivitiesToday");
        }
    }
}
