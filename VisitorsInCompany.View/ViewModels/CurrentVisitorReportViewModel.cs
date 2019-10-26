using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using VisitorsInCompany.Interfaces;

namespace VisitorsInCompany.ViewModels
{
   class CurrentVisitorReportViewModel : MvxViewModel
   {
      private readonly IMvxNavigationService _navigationService;
      private readonly IRepository _repo;
      public CurrentVisitorReportViewModel(IMvxNavigationService navigationService, IRepository repo)
      {
         _navigationService = navigationService;
         _repo = repo;
      }

      private ObservableCollection<VisitorViewModel> _visitors;
      public ObservableCollection<VisitorViewModel> Visitors
      {
         get => _visitors;
         private set
         {
            _visitors = value;
            RaisePropertyChanged(() => Visitors);
         }
      }

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
         FillInVisitors();
         await base.Initialize();
      }

      private void FillInVisitors()
      {
         List<VisitorViewModel> vvm = new List<VisitorViewModel>();
         foreach (var item in _repo.GetNotExitVisitors())
            vvm.Add(new VisitorViewModel(item));

         Visitors = new ObservableCollection<VisitorViewModel>(vvm);
      }

      public void SortCollection(string text)
      {
         text = text.ToLower();
         FillInVisitors();
         if(!string.IsNullOrWhiteSpace(text))
         {
            Visitors = new ObservableCollection<VisitorViewModel>(Visitors.Where(v => v.FullName.ToLower().Contains(text)));
         }
         
      }

      public IMvxAsyncCommand GoToMainScreenCommand => new MvxAsyncCommand(GoToMainScreenAsync);
      private async Task GoToMainScreenAsync()
      {
         await _navigationService.Navigate<MainScreenViewModel>();
      }

      public IMvxAsyncCommand GoToReportViewCommand => new MvxAsyncCommand(GoToReportViewAsync);
      private async Task GoToReportViewAsync()
      {
         await _navigationService.Navigate<ReportsViewModel>();
      }

      public IMvxCommand ExitVisitorCommand => new MvxCommand(ExitVisitorAsync);
      private void ExitVisitorAsync()
      {
         if (MessageBox.Show("Установить факт выхода посетителя?", "Подтвердите своё действие", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
         {
            CurrentVisitor.Visitor.ExitTime = DateTime.Now.ToString();
            _repo.RemoveFromOrganization(CurrentVisitor.Visitor);
            Visitors.Remove(CurrentVisitor);
         }
         //await _navigationService.Navigate<MainScreenViewModel>();
      }
   }
}
