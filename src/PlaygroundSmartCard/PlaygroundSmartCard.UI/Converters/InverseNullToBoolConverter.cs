using System;
using System.Globalization;
using System.Windows.Data;

namespace PlaygroundSmartCard.UI.Converters
{
    /// <summary>
    /// Converts a value to its inverse boolean representation based on nullity.
    /// </summary>
    public class InverseNullToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value to its inverse boolean representation based on nullity.
        /// </summary>
        /// <param name="value">The value to be converted.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture to be used in the conversion.</param>
        /// <returns>True if the value is not null; otherwise, false.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
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