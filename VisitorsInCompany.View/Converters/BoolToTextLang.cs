using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VisitorsInCompany.Converters
{
   public class BoolToTextLang : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if(value is bool flag)
         {
            if (flag)
               return "RU";
            return "EN";
         }
         return null;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
   }
}
