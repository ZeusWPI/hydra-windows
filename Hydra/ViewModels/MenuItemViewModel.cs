using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hydra.ViewModels {

    public class MenuItemViewModel : ViewModelBase, INotifyPropertyChanged {

        public string DisplayName { get; set; }

        public string IconSource { get; set; }

        public ICommand Command { get; set; }

        public override string ToString() {
            return DisplayName;
        }
    }
}
