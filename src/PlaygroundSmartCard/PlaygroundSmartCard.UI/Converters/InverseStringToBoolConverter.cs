﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace PlaygroundSmartCard.UI.Converters
{
    /// <summary>
    /// Converts a string value to its inverse boolean representation.
    /// </summary>
    public class InverseStringToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Converts a string value to its inverse boolean representation.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>True if the string value is null or empty, false otherwise.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value is string stringValue && string.IsNullOrEmpty(stringValue))
            {
                return true;
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