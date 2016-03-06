using Hydra.DataSources;
using Hydra.Exceptions;
using Hydra.Models.Activities;
using Hydra.ViewModels.Common;
using Hydra.Views.Activities;
using Hydra.Views.Common;
using Prism.Commands;
using Prism.Windows.AppModel;
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
    public class ActivitiesPageViewModel : AbstractPageViewModel, INotifyPropertyChanged {

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

        public ActivitiesPageViewModel(IResourceLoader resourceLoader, IActivitySource activitySource) : base(resourceLoader) {
            this.activitySource = activitySource;
            EventDays = new ObservableCollection<EventDay>();
            GetActivities();
        }

        public async void GetActivities() {
            try {
                IEnumerable<EventDay> days = await activitySource.GetActivitiesByDate();
                foreach (EventDay day in days) EventDays.Add(day);
                OnPropertyChanged();
            } catch (DataSourceException ex) {
                await ErrorDialogFactory.NetworkErrorDialog().ShowAsync();
            }
        }
    }
}
