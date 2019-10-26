using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VisitorsInCompany.Data;
using VisitorsInCompany.Interfaces;
using VisitorsInCompany.Views;

namespace VisitorsInCompany.ViewModels
{
   class MainScreenViewModel : MvxViewModel
   {
      private readonly IMvxNavigationService _navigationService;
      private readonly IRepository _repo;
      public MainScreenViewModel(IMvxNavigationService navigationService, IRepository repo)
      {
         _navigationService = navigationService;
         _repo = repo;
      }

      public override async Task Initialize()
      {
         await base.Initialize();
      }

      private bool _isVisibleButton;
      public bool IsVisibleButton
      {
         get => _isVisibleButton;
         set
         {
            _isVisibleButton = value;
            RaisePropertyChanged(() => IsVisibleButton);
         }
      }

      internal void ChangeVisibilityButton()
      {
         IsVisibleButton = !IsVisibleButton;
      }

      /// <summary>
      /// Создать пользователя
      /// </summary>
      public IMvxAsyncCommand GoToCreateUserCommand => new MvxAsyncCommand(GoToCreateUserAsync);
      private async Task GoToCreateUserAsync()
      {
         await _navigationService.Navigate<CreateUserViewModel>();
      }

      /// <summary>
      /// Удалить пользователя из организации
      /// </summary>
      public IMvxAsyncCommand GoToVisitorsListCommand => new MvxAsyncCommand(GoToVisitorsListAsync);
      private async Task GoToVisitorsListAsync()
      {
         await _navigationService.Navigate<VisitorsListViewModel>();
      }

      /// <summary>
      /// Показать пользователей в организации
      /// </summary>
      public IMvxAsyncCommand GoToVisitorsListFullInfoCommand => new MvxAsyncCommand(GoToVisitorsListFullInfoAsync);
      private async Task GoToVisitorsListFullInfoAsync()
      {
         await _navigationService.Navigate<VisitorsListFullInfoViewModel>();
      }

      /// <summary>
      /// Вход администратора
      /// </summary>
      public IMvxAsyncCommand GoToLoginCommand => new MvxAsyncCommand(GoToLoginAsync);
      private async Task GoToLoginAsync()
      {
         await _navigationService.Navigate<LoginViewModel>();
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
         CultureInfo ci;
         if (InputLanguageManager.Current.CurrentInputLanguage.Name == "ru-RU")
         {
            ci = new CultureInfo("en-US");
            InputLanguageManager.Current.CurrentInputLanguage = ci;
            IsRussian = false;
            return;
         }

         ci = new CultureInfo("ru-RU");
         InputLanguageManager.Current.CurrentInputLanguage = ci;
         IsRussian = true;
      }
   }
}
