using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VisitorsInCompany.Helpers;
using VisitorsInCompany.Interfaces;
using VisitorsInCompany.Models;

namespace VisitorsInCompany.ViewModels
{
   public class LoginViewModel : MvxViewModel
   {
      private readonly IMvxNavigationService _navigationService;
      private readonly IRepository _repo;
      private INIManager _iniManager;

      public LoginViewModel(IMvxNavigationService navigationService, IRepository repo)
      {
         _navigationService = navigationService;
         _repo = repo;
         Admin = new Admin();
         _iniManager = new INIManager($@"{Directory.GetCurrentDirectory()}\conf\config.ini");
      }

      public override async Task Initialize()
      {
         OperationSystemInteractiveHelper.StartVirtualKeyboard();
         await base.Initialize();
      }

      private Admin _admin;
      public Admin Admin
      {
         get => _admin;
         set
         {
            _admin = value;
            RaisePropertyChanged(() => Admin);
         }
      }

      public IMvxAsyncCommand<PasswordBox> LoginCommand => new MvxAsyncCommand<PasswordBox>(LoginAsync);
      private async Task LoginAsync(PasswordBox password)
      {
         Admin.Password = password?.Password;
         string login = _iniManager.GetPrivateString("UserData", "login");
         string pass = _iniManager.GetPrivateString("UserData", "password");

         if (login == string.Empty || pass == string.Empty)
         {
            MessageBox.Show("Проверте данные в файле config");
            return;
         }

         if (Admin.Login == login && Admin.Password == pass)
         {
            Helper.KillOSKProcess();
            await _navigationService.Navigate<ReportsViewModel>();
         }
         else
            MessageBox.Show("Вы ввели некорректные данные");

         login = null;
         pass = null;
      }

      public IMvxAsyncCommand GoToMainScreenCommand => new MvxAsyncCommand(GoToMainScreenAsync);
      private async Task GoToMainScreenAsync()
      {
         Helper.KillOSKProcess();
         await _navigationService.Navigate<MainScreenViewModel>();
      }
   }
}
