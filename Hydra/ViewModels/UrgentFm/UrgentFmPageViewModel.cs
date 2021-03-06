﻿using Hydra.DataSources;
using Hydra.Models.Schamper;
using Hydra.ViewModels.Common;
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
using System.Windows.Input;

namespace Hydra.ViewModels.UrgentFm {
    public class UrgentFmPageViewModel : AbstractPageViewModel, INotifyPropertyChanged {

        public Uri UrgentStreamUrl { get { return new Uri("http://195.10.2.96/urgent/high.mp3"); } }

        public bool Playing { get; set; } = false;
        public ICommand PlayPauseCommand { get; set; }

        public UrgentFmPageViewModel(IResourceLoader resourceLoader) : base(resourceLoader) {
            PlayPauseCommand = new DelegateCommand(() => {
                this.Playing = !this.Playing;
                OnPropertyChanged();
            });
        }
        
    }
}
