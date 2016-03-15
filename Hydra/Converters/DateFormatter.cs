using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Hydra.Converters {
    /// <summary>
    /// Formats DateTime objects into a given format string
    /// </summary>
    public class DateFormatter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, string language) {
            string formatString = parameter as string;
            if (!string.IsNullOrEmpty(formatString)) {
                return string.Format(new CultureInfo("nl-be"), formatString, value);
            }

            // If the format string is null or empty, simply call ToString() on the value.
            return value.ToString();
        }

        // No need to implement converting back on a one-way binding 
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
