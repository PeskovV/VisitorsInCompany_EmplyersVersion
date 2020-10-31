
namespace VisitorsInCompany.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class BoolToTextLang : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool flag)
                return flag ? "RU" : "EN";

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
