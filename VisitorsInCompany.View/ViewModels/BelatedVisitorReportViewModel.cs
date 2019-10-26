using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VisitorsInCompany.Interfaces;

namespace VisitorsInCompany.ViewModels
{
   class BelatedVisitorReportViewModel : MvxViewModel
   {
      private readonly IMvxNavigationService _navigationService;
      private readonly IRepository _repo;
      public BelatedVisitorReportViewModel(IMvxNavigationService navigationService, IRepository repo)
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

      private DateTime _firstDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0,0,0) ;
      public DateTime FirstDate
      {
         get => _firstDate;
         set
         {
            _firstDate = value;
            RaisePropertyChanged(() => FirstDate);
         }
      }

      private DateTime _secondDate = DateTime.Now;
      public DateTime SecondDate
      {
         get => _secondDate;
         set
         {
            _secondDate = value;
            RaisePropertyChanged(() => SecondDate);
         }
      }

      private bool _formedData;
      public bool FormedData
      {
         get => _formedData;
         set
         {
            _formedData = value;
            RaisePropertyChanged(() => FormedData);
         }
      }

      public override async Task Initialize()
      {
         await base.Initialize();
      }

      private void FillInVisitors()
      {
         List<VisitorViewModel> vvm = new List<VisitorViewModel>();
         foreach (var item in _repo.GetNotExitVisitors().Where(v => (DateTime.Parse(v.EntryTime).Date >= FirstDate) && (DateTime.Parse(v.EntryTime).Date <= SecondDate)))
            vvm.Add(new VisitorViewModel(item));

         Visitors = new ObservableCollection<VisitorViewModel>(vvm);
      }

      public void SortCollection(string text)
      {
         text = text.ToLower();
         FillInVisitors();
         if (!string.IsNullOrWhiteSpace(text))
            Visitors = new ObservableCollection<VisitorViewModel>(Visitors.Where(v => v.FullName.ToLower().Contains(text)));
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

      public IMvxCommand FormDataCommand => new MvxCommand(FormDataAsync);
      private void FormDataAsync()
      {
         if (FirstDate > SecondDate)
            MessageBox.Show("Некорректный формат даты");
         else
         {
            FillInVisitors();
            FormedData = true;
         }
      }
   }
}
