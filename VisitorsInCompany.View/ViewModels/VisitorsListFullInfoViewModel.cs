using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsInCompany.Interfaces;

namespace VisitorsInCompany.ViewModels
{
   public class VisitorsListFullInfoViewModel : MvxViewModel
   {
      private readonly IMvxNavigationService _navigationService;
      private readonly IRepository _repo;
      public VisitorsListFullInfoViewModel(IMvxNavigationService navigationService, IRepository repo)
      {
         _navigationService = navigationService;
         _repo = repo;
         //VisitorVM = new VisitorViewModel();
      }

      public ObservableCollection<VisitorViewModel> Visitors { get; private set; }

      private VisitorViewModel _currentVisitor;
      public VisitorViewModel CurrentVisitor
      {
         get => _currentVisitor;
         set
         {
            _currentVisitor = value;
            RaisePropertyChanged(() => CurrentVisitor);
         }
      }

      public override async Task Initialize()
      {
         List<VisitorViewModel> vvm = new List<VisitorViewModel>();
         foreach (var item in _repo.GetNotExitVisitors())
            vvm.Add(new VisitorViewModel(item));

         Visitors = new ObservableCollection<VisitorViewModel>(vvm);
         await base.Initialize();
      }

      public IMvxAsyncCommand ToMainScreenCommand => new MvxAsyncCommand(ToMainScreenAsync);
      private async Task ToMainScreenAsync()
      {
         await _navigationService.Navigate<MainScreenViewModel>();
      }
   }
}
