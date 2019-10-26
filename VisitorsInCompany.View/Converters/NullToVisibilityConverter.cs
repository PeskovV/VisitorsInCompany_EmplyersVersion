using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VisitorsInCompany.Models;
using VisitorsInCompany.ViewModels;

namespace VisitorsInCompany.Converters
{
   class NullToVisibilityConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         VisitorViewModel visitor = value as VisitorViewModel;
         return visitor != null ? Visibility.Visible : Visibility.Collapsed;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
   }
}
