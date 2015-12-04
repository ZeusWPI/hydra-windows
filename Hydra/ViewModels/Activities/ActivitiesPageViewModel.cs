using Hydra.DataSources;
using Hydra.Models.Activities;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Hydra.ViewModels.Activities {
    public class ActivitiesPageViewModel : ViewModelBase, INotifyPropertyChanged {

        private readonly IAssociationSource associationSource;
        private readonly IActivitySource activitySource;

        public ObservableCollection<Konvent> Konvents { get; set; }
        public ObservableCollection<EventDay> EventDays { get; set; }

        public ActivitiesPageViewModel(IAssociationSource associationSource, IActivitySource activitySource) {
            this.associationSource = associationSource;
            this.activitySource = activitySource;
            Konvents = new ObservableCollection<Konvent>();
            EventDays = new ObservableCollection<EventDay>();

            GetAssociations();
            GetActivities();
        }

        public async Task GetAssociations() {
            IEnumerable<Konvent> konvents = await associationSource.GetAssociationsByKonvent();
            foreach (Konvent konvent in konvents) Konvents.Add(konvent);
            OnPropertyChanged();
        }

        public async Task GetActivities() {
            IEnumerable<EventDay> days = await activitySource.GetActivitiesByDate();
            foreach (EventDay day in days) EventDays.Add(day);
            OnPropertyChanged();
        }
    }
}
