using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.Platforms.Wpf.Views;
using VisitorsInCompanyMain;

namespace VisitorsInCompany.View
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : MvxApplication
   {
      protected override void RegisterSetup()
      {
         //Mvx.IoCProvider.LazyConstructAndRegisterSingleton<AppContext, AppContext>();
         //Mvx.IoCProvider.RegisterType<IRepository, VisitorRepository>();
         this.RegisterSetupType<MvxWpfSetup<CustomApp>>();
         
      }
   }
}
