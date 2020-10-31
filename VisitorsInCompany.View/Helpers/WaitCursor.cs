
namespace VisitorsInCompany.Helpers
{
    using System;
    using System.Windows.Input;

    public class WaitCursor : LocalCursor
    {
        public WaitCursor() : base(Cursors.Wait) { }
    }

    public class LocalCursor : IDisposable
    {
        protected Cursor OuterCursor { get; set; }

        public LocalCursor(Cursor localCursor)
        {
            OuterCursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = localCursor;
        }

        public virtual void Dispose() =>
           Mouse.OverrideCursor = OuterCursor;
    }
}
