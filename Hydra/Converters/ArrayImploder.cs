using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Hydra.Converters {
    /// <summary>
    /// Converter that translates an array of strings to a single comma-separated list.
    /// </summary>
    public sealed class ArrayImploder : IValueConverter {
        // Default separator between two items
        private readonly string DEFAULT_SEPARATOR = ", ";

        public object Convert(object value, Type targetType, object parameter, string language) {
            object[] valueArray = value as object[];

            if(valueArray != null) {
                string separator = (parameter as string) ?? DEFAULT_SEPARATOR;
                return string.Join(separator, valueArray);
            }

            return valueArray.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            string valueString = value as string;

            if (valueString != null) {
                string separator = (parameter as string) ?? DEFAULT_SEPARATOR;
                return valueString.Split(separator.ToCharArray());
            }

            return new string[] { value.ToString() };
        }
    }
}
