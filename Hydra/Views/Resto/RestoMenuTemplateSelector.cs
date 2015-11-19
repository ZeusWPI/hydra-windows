using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Hydra.Models.Resto;

namespace Hydra.Views.Resto {
    public class RestoMenuTemplateSelector : DataTemplateSelector {
        public DataTemplate LoadingTemplate { get; set; }
        public DataTemplate RestoClosedTemplate { get; set; }
        public DataTemplate DayMenuTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item) {

            if (item is DailyMenu) {
                DailyMenu menu = (DailyMenu) item;
                if (menu.Open) {
                    return DayMenuTemplate;
                } else {
                    return RestoClosedTemplate;
                }
            }

            return LoadingTemplate;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) {
            return SelectTemplateCore(item);
        }
    }
}
