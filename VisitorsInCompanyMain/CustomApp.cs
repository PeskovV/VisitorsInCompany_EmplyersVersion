using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace VisitorsInCompanyMain
{
   class CustomApp : MvxApplication
   {
      public override void Initialize()
      {
         Mvx.IoCProvider.ConstructAndRegisterSingleton<AppDbContext, AppDbContext>();
         Mvx.IoCProvider.RegisterType<IRepository, VisitorRepository>();
         RegisterCustomAppStart<AppStart>();
      }
   }
}
