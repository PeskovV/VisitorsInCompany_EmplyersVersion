using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VisitorsInCompany.Data;
using VisitorsInCompany.Helpers;
using VisitorsInCompany.Interfaces;
using VisitorsInCompany.Models;

namespace VisitorsInCompany.ViewModels
{
   class CreateUserViewModel : MvxViewModel
   {
      private readonly IMvxNavigationService _navigationService;
      private readonly IRepository _repo;
      public CreateUserViewModel(IMvxNavigationService navigationService, IRepository repo)
      {
         _navigationService = navigationService;
         _repo = repo;
         VisitorVM = new VisitorViewModel();
      }

      public VisitorViewModel VisitorVM { get; set; }

      public override async Task Initialize()
      {
         await base.Initialize();
         OperationSystemInteractiveHelper.StartVirtualKeyboard();
      }

      public IMvxAsyncCommand GoToMainScreenCommand => new MvxAsyncCommand(GoToMainScreenAsync);
      private async Task GoToMainScreenAsync()
      {
         Helper.KillOSKProcess();
         await _navigationService.Navigate<MainScreenViewModel>();
      }

      public IMvxAsyncCommand CreateUserCommand => new MvxAsyncCommand(CreateUserAsync);
      private async Task CreateUserAsync()
      {
         if(!VerifyFullData(VisitorVM.Visitor))
         {
            MessageBox.Show("Заполните все поля\\ Fill all fields", "Warning");
            return;
         }
         if(_repo.VerifyExitVisitor(VisitorVM.Visitor.FirstName, VisitorVM.Visitor.LastName, VisitorVM.Visitor.Organization))
         {
            VisitorVM.Visitor.EntryTime = DateTime.Now.ToString();
            _repo.Add(VisitorVM.Visitor);
            await GoToMainScreenAsync();
         }
         else
            MessageBox.Show("Просим, вас, обратиться к службе безопасности", "Пользователь в организации");
      }

      private bool VerifyFullData(Visitor visitor)
      {
         if(string.IsNullOrWhiteSpace(visitor.FirstName) 
            || string.IsNullOrWhiteSpace(visitor.LastName) 
            || string.IsNullOrWhiteSpace(visitor.Organization) 
            || string.IsNullOrWhiteSpace(visitor.VisitGoal)
            || string.IsNullOrWhiteSpace(visitor.Attendant))
         {
            return false;
         }
         return true;
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
