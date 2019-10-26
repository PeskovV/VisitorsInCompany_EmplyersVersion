using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VisitorsInCompany.Helpers;
using VisitorsInCompany.Interfaces;
using VisitorsInCompany.Models;

namespace VisitorsInCompany.ViewModels
{
   class VisitorsListViewModel : MvxViewModel
   {
      private readonly IMvxNavigationService _navigationService;
      private readonly IRepository _repo;
      public VisitorsListViewModel(IMvxNavigationService navigationService, IRepository repo)
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

      public IMvxAsyncCommand ExitVisitorCommand => new MvxAsyncCommand(ExitVisitorAsync);
      private async Task ExitVisitorAsync()
      {
         if(MessageBox.Show("Вы уверены?\\Are you shure?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
         {
            CurrentVisitor.Visitor.ExitTime = DateTime.Now.ToString();
            _repo.RemoveFromOrganization(CurrentVisitor.Visitor);
            Visitors.Remove(CurrentVisitor);
            await _navigationService.Navigate<MainScreenViewModel>();
         }
         //await _navigationService.Navigate<MainScreenViewModel>();
      }

      public IMvxAsyncCommand ToMainScreenCommand => new MvxAsyncCommand(ToMainScreenAsync);
      private async Task ToMainScreenAsync()
      {
         await _navigationService.Navigate<MainScreenViewModel>();
      }

      private bool _isRussian = InputLanguageManager.Current.CurrentInputLanguage.Name == "ru-RU";
      public bool IsRussian
      {
         get => _isRussian;
         set
         {
            _isRussian = value;
            RaisePropertyChanged(() => IsRussian);
         }
      }

      public IMvxCommand ChangeLangCommand => new MvxCommand(ChangeLang);
      private void ChangeLang()
      {
         if (InputLanguageManager.Current.CurrentInputLanguage.Name == "ru-RU")
         {
            InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo("en-US");
            IsRussian = false;
            return;
         }
         InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo("ru-RU");
         IsRussian = true;
      }

      public IMvxCommand ShowKeyboardCommand => new MvxCommand(ShowKeyboard);
      private void ShowKeyboard()
      {
         OperationSystemInteractiveHelper.StartVirtualKeyboard();
      }
   }
}
