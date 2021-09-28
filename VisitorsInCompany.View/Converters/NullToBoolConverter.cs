using System;
using System.Globalization;
using System.Windows.Data;
using VisitorsInCompany.View.ViewModels;

namespace VisitorsInCompany.View.Converters
{
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
           value is VisitorViewModel;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
              throw new NotImplementedException();
    }
}
