using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VisitorsInCompany.Helpers
{
   public class LocalCursor :IDisposable
   {
      protected Cursor OuterCursor { get; set; }

      public LocalCursor(Cursor localCursor)
      {
         OuterCursor = Mouse.OverrideCursor;
         Mouse.OverrideCursor = localCursor;
      }

      public virtual void Dispose()
      {
         Mouse.OverrideCursor = OuterCursor;
      }
   }

   class WaitCursor : LocalCursor
   {
      public WaitCursor() : base(Cursors.Wait)
      {
      }
   }
}
