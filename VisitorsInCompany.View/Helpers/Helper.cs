
namespace VisitorsInCompany.Helpers
{
    using System.Diagnostics;

    public static class Helper
    {
        private static int _timeBeforeSearch = 2000;

        internal static int GetTimeBeforeSearch() => 
            _timeBeforeSearch;

        internal static void KillOSKProcess()
        {
            foreach (var item in Process.GetProcessesByName("osk"))
                item.Kill();
        }
    }
}
