using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsInCompany.ViewModels;

namespace VisitorsInCompany
{
   public class AppStart : MvxAppStart
   {
      private readonly IMvxNavigationService _navigationService;

      public AppStart(
       IMvxApplication application,
       IMvxNavigationService navigationService) : base(application, navigationService)
      {
         _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
      }

      protected override async Task NavigateToFirstViewModel(object hint = null)
      {
         await _navigationService.Navigate<MainScreenViewModel>();
      }

   }
}
