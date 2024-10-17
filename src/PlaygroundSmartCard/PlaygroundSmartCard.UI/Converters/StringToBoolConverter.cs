using System;
using System.Globalization;
using System.Windows.Data;

namespace PlaygroundSmartCard.UI.Converters
{
    /// <summary>
    /// Converts a string value to a boolean value.
    /// </summary>
    public class StringToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Converts a string value to a boolean value.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A boolean value indicating whether the string value is null or empty.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return !string.IsNullOrEmpty(stringValue);
            }

            return false;
        }

        /// <summary>
        /// This method is not implemented and will always throw a <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="value">The value to be converted back.</param>
        /// <param name="targetType">The type of the binding source property.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture to be used in the conversion.</param>
        /// <returns>This method always throws a <see cref="NotImplementedException"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}