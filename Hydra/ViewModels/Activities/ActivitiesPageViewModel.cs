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

        public ObservableCollection<Association> Associations { get; set; }

        public ActivitiesPageViewModel(IAssociationSource associationSource) {
            this.associationSource = associationSource;
            Associations = new ObservableCollection<Association>();

            GetAssociations();
        }

        public async Task GetAssociations() {
            IEnumerable<Association> associations = await associationSource.GetAssociations();
            foreach (Association association in associations) Associations.Add(association);
            OnPropertyChanged();
        }
    }
}
