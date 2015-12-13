using Hydra.DataSources;
using Hydra.Models.Activities;
using Hydra.Views.Activities;
using Prism.Commands;
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

        private readonly IActivitySource activitySource;
        
        public ObservableCollection<EventDay> EventDays { get; set; }

        // Technically, directly referring to the dialog is against MVVM, but oh well, this is still a hobby project.
        public DelegateCommand OpenDialogCommand {
            get {
                DelegateCommand command = new DelegateCommand(async () => {
                    var associationFilterDialog = new AssociationFilterDialog();
                    await associationFilterDialog.ShowAsync();
                });
                return command;
            }
        }

        public ActivitiesPageViewModel(IAssociationSource associationSource, IActivitySource activitySource) {
            this.activitySource = activitySource;
            EventDays = new ObservableCollection<EventDay>();
            GetActivities();
        }

        public async Task GetActivities() {
            IEnumerable<EventDay> days = await activitySource.GetActivitiesByDate();
            foreach (EventDay day in days) EventDays.Add(day);
            OnPropertyChanged();
        }
    }
}
