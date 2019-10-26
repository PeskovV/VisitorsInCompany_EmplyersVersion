using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsInCompany.Helpers
{
   public static class Helper
   {
      private static int _timeBeforeSearch = 2000;

      internal static int GetTimeBeforeSearch() => _timeBeforeSearch;

      internal static void KillOSKProcess()
      {
         foreach (var item in Process.GetProcessesByName("osk"))
            item.Kill();
      }
   }
}
