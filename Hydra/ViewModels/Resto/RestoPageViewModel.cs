﻿using Hydra.DataSources;
using Hydra.Models.Resto;
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

namespace Hydra.ViewModels.Resto {
    public class RestoPageViewModel : ViewModelBase, INotifyPropertyChanged {

        private readonly IRestoSource restoSource;

        public ObservableCollection<DailyMenu> RestoInfoList { get; set; }

        public ObservableCollection<RestoLegendItem> Legend { get; set; }

        public RestoPageViewModel(IRestoSource restoSource) {
            this.restoSource = restoSource;
            RestoInfoList = new ObservableCollection<DailyMenu>();
            Legend = new ObservableCollection<RestoLegendItem>();

            GetRestoMenus();
            GetRestoLegend();
        }

        public async Task GetRestoMenus() {
            IEnumerable<DailyMenu> restoMenus = await restoSource.GetRestoMenusThisWeek();
            foreach (DailyMenu menu in restoMenus) RestoInfoList.Add(menu);
            OnPropertyChanged();
        }

        public async Task GetRestoLegend() {
            IEnumerable<RestoLegendItem> legendItems = await restoSource.GetRestoLegendItems();
            foreach (RestoLegendItem legendItem in legendItems) Legend.Add(legendItem);
            OnPropertyChanged();
        }
    }
}