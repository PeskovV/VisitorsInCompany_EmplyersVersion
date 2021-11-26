using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VisitorsInCompany.Helpers;

namespace VisitorsInCompany.View.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly INIManager _iniManager;

        public IMvxAsyncCommand GoToMainScreenCommand => new MvxAsyncCommand(GoToMainScreenAsync);
        public IMvxAsyncCommand<PasswordBox> LoginCommand => new MvxAsyncCommand<PasswordBox>(LoginAsync);

        private AdminViewModel _admin;
        public AdminViewModel Admin
        {
            get => _admin;
            set
            {
                _admin = value;
                RaisePropertyChanged(() => Admin);
            }
        }

        public LoginViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            Admin = new AdminViewModel();
            _iniManager = new INIManager($@"{Directory.GetCurrentDirectory()}\conf\config.ini");
        }

        public override async Task Initialize()
        {
            OperationSystemInteractiveHelper.StartVirtualKeyboard();
            await base.Initialize();
        }

        private async Task LoginAsync(PasswordBox password)
        {
            string login = _iniManager.GetPrivateString("UserData", "login");
            string pass = _iniManager.GetPrivateString("UserData", "password");

            if (login == string.Empty || pass == string.Empty)
            {
                MessageBox.Show($@"Проверте реквизиты в файле { Directory.GetCurrentDirectory()}\conf\config.ini");
                return;
            }

            Admin.Password = password?.Password;
            if (Admin.Login == login && Admin.Password == pass)
            {
                Helper.KillOSKProcess();
                await _navigationService.Navigate<ReportsViewModel>();
            }
            else
                MessageBox.Show("Вы ввели некорректные данные");
        }

        private async Task GoToMainScreenAsync()
        {
            Helper.KillOSKProcess();
            await _navigationService.Navigate<MainScreenViewModel>();
        }
    }
}
