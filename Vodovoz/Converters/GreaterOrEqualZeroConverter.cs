﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Vodovoz.Converters
{
    public class GreaterOrEqualZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value >= 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}