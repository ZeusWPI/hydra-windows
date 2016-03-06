using Hydra.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.ViewModels.Info {
    public class InfoPageItemViewModel {

        public string Title { get; set; }

        public ObservableCollection<ButtonViewModel> Links { get; set; }
    }
}
