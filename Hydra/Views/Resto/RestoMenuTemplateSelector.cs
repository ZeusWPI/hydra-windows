using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Hydra.Models.Resto;

namespace Hydra.Views.Resto {

    /// <summary>
    /// Selects the datatemplate that should be used for the resto information.
    /// Can be used in a Pivot for both header and item
    /// </summary>
    public class RestoMenuTemplateSelector : DataTemplateSelector {
        public DataTemplate LoadingTemplate { get; set; }
        public DataTemplate RestoClosedTemplate { get; set; }
        public DataTemplate DayMenuTemplate { get; set; }
        public DataTemplate SandwichMenuTemplate { get; set; }
        public DataTemplate RestoMapTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item) {
            if(item == null) {
                return LoadingTemplate;
            }

            if (item is DailyMenu) {
                DailyMenu menu = (DailyMenu) item;
                return (menu.Open) ? DayMenuTemplate : RestoClosedTemplate;
            }
            if(item is SandwichMenu) {
                return SandwichMenuTemplate;
            }
            if (item is RestoMap) {
                return RestoMapTemplate;
            }

            // Fall back
            return LoadingTemplate;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) {
            return SelectTemplateCore(item);
        }
    }
}
