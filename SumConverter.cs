using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Atestat4.Models;

namespace Atestat4.Converters
{
    public class SumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = value as IEnumerable<Company>;
            var sum = list?.Sum(c => c.FixedFee) ?? 0;
            return sum.ToString("N0", CultureInfo.InvariantCulture) + " RON";
        }
        public object ConvertBack(object v, Type t, object p, CultureInfo c)
          => throw new NotImplementedException();
    }
}
