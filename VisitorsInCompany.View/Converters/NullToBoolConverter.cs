
namespace VisitorsInCompany.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using VisitorsInCompany.ViewModels;

    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
           value as VisitorViewModel != null;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
              throw new NotImplementedException();
    }
}
