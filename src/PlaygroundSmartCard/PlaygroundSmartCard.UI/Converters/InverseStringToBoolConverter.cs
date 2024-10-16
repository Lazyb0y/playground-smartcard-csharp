using System;
using System.Globalization;
using System.Windows.Data;

namespace PlaygroundSmartCard.UI.Converters
{
    public class InverseStringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value is string stringValue && string.IsNullOrEmpty(stringValue))
            {
                return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && boolValue)
            {
                return null;
            }

            return string.Empty;
        }
    }
}