using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VisitorsInCompany.ViewModels;

namespace VisitorsInCompany.Converters
{
   class NullToBoolConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         VisitorViewModel visitor = value as VisitorViewModel;
         return visitor != null ? true : false;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
   }
}
