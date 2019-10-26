using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsInCompany.Data;
using VisitorsInCompany.Interfaces;
using VisitorsInCompany.Models;

namespace VisitorsInCompany
{
   class CustomApp : MvxApplication
   {
      public override void Initialize()
      {
         Mvx.IoCProvider.ConstructAndRegisterSingleton<AppDbContext, AppDbContext>();
         Mvx.IoCProvider.RegisterType<IRepository, VisitorRepository>();
         RegisterCustomAppStart<AppStart>();
         //RegisterAppStart<MainScreenViewModel>();
      }
   }
}
