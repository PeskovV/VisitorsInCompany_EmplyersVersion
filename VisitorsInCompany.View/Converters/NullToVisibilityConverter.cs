using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using VisitorsInCompany.View.ViewModels;

namespace VisitorsInCompany.View.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is VisitorViewModel ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
