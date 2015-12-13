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
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Hydra.ViewModels.Activities {
    public class AssociationFilterDialogViewModel : ViewModelBase, INotifyPropertyChanged {

        private readonly IAssociationSource associationSource;

        public ObservableCollection<Konvent> Konvents { get; set; }

        public ICommand SaveButtonCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }

        public AssociationFilterDialogViewModel(IAssociationSource associationSource) {
            this.associationSource = associationSource;
            Konvents = new ObservableCollection<Konvent>();

            GetAssociations();
        }

        public async Task GetAssociations() {
            IEnumerable<Konvent> konvents = await associationSource.GetAssociationsByKonvent();
            foreach (Konvent konvent in konvents) Konvents.Add(konvent);
            OnPropertyChanged();
        }
    }
}
