﻿using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Hydra.ViewModels {

    public class MenuItemViewModel : ViewModelBase {

        public string DisplayName { get; set; }

        public Symbol SymbolIcon { get; set; }

        public ICommand Command { get; set; }

        public override string ToString() {
            return DisplayName;
        }
    }
}
