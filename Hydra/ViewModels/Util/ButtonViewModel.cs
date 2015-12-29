using Prism.Commands;
using Prism.Windows.Mvvm;
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
    public class ButtonViewModel : ViewModelBase, INotifyPropertyChanged {

        /// <summary>
        /// The text for the button
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The path to the icon image.
        /// </summary>
        public string IconSource { get; set; }

        /// <summary>
        /// The command that should be used on click/touch/... (e.g. navigate to page)
        /// </summary>
        public ICommand Command { get; set; }

        public override string ToString() {
            return DisplayName;
        }
    }
}
