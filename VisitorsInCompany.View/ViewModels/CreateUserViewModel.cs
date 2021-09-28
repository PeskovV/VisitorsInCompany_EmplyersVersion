using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VisitorsInCompany.Helpers;

namespace VisitorsInCompany.View.ViewModels
{
    public class CreateUserViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IRepository _repo;

        private bool _isRussian = InputLanguageManager.Current.CurrentInputLanguage.Name == "ru-RU";

        public IMvxAsyncCommand GoToMainScreenCommand => new MvxAsyncCommand(GoToMainScreenAsync);
        public IMvxAsyncCommand CreateUserCommand => new MvxAsyncCommand(CreateUserAsync);
        public IMvxCommand ChangeLangCommand => new MvxCommand(ChangeLang);
        public IMvxCommand ShowKeyboardCommand => new MvxCommand(ShowKeyboard);

        public VisitorViewModel VisitorVM { get; set; }

        public bool IsRussian
        {
            get => _isRussian;
            set
            {
                _isRussian = value;
                RaisePropertyChanged(() => IsRussian);
            }
        }


        public CreateUserViewModel(IMvxNavigationService navigationService, IRepository repo)
        {
            _navigationService = navigationService;
            _repo = repo;
            VisitorVM = new VisitorViewModel();
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            OperationSystemInteractiveHelper.StartVirtualKeyboard();
        }

        private async Task GoToMainScreenAsync()
        {
            Helper.KillOSKProcess();
            await _navigationService.Navigate<MainScreenViewModel>();
        }

        private async Task CreateUserAsync()
        {
            if (!VerifyFullData(VisitorVM.Visitor))
            {
                MessageBox.Show("Заполните все поля\\ Fill all fields", "Warning");
                return;
            }
            if (_repo.VerifyExitVisitor(VisitorVM.Visitor))
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
            var result =
                string.IsNullOrWhiteSpace(visitor.FirstName)
             || string.IsNullOrWhiteSpace(visitor.LastName)
             || string.IsNullOrWhiteSpace(visitor.Organization)
             || string.IsNullOrWhiteSpace(visitor.VisitGoal)
             || string.IsNullOrWhiteSpace(visitor.Attendant);

            return !result;
        }

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

        private void ShowKeyboard() =>
            OperationSystemInteractiveHelper.StartVirtualKeyboard();
    }
}
