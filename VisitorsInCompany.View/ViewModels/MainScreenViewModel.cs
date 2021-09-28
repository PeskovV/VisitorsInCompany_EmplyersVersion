using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VisitorsInCompany.View.ViewModels
{
    public class MainScreenViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public IMvxAsyncCommand GoToCreateUserCommand => new MvxAsyncCommand(GoToCreateUserAsync);
        public IMvxAsyncCommand GoToVisitorsListCommand => new MvxAsyncCommand(GoToVisitorsListAsync);
        public IMvxAsyncCommand GoToVisitorsListFullInfoCommand => new MvxAsyncCommand(GoToVisitorsListFullInfoAsync);
        public IMvxCommand ChangeLangCommand => new MvxCommand(ChangeLang);
        public IMvxAsyncCommand GoToLoginCommand => new MvxAsyncCommand(GoToLoginAsync);

        private bool _isVisibleButton;
        private bool _isRussian = InputLanguageManager.Current.CurrentInputLanguage.Name == "ru-RU";

        public bool IsVisibleButton
        {
            get => _isVisibleButton;
            set
            {
                _isVisibleButton = value;
                RaisePropertyChanged(() => IsVisibleButton);
            }
        }

        public bool IsRussian
        {
            get => _isRussian;
            set
            {
                _isRussian = value;
                RaisePropertyChanged(() => IsRussian);
            }
        }

        public MainScreenViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override async Task Initialize() => await base.Initialize();
        internal void ChangeVisibilityButton() => IsVisibleButton = !IsVisibleButton;

        private void ChangeLang()
        {
            CultureInfo ci;
            switch (InputLanguageManager.Current.CurrentInputLanguage.Name)
            {
                case "ru-RU":
                    ci = new CultureInfo("en-US");
                    InputLanguageManager.Current.CurrentInputLanguage = ci;
                    IsRussian = false;
                    break;
                default:
                    ci = new CultureInfo("ru-RU");
                    InputLanguageManager.Current.CurrentInputLanguage = ci;
                    IsRussian = true;
                    break;
            }
        }

        private async Task GoToCreateUserAsync() => await _navigationService.Navigate<CreateUserViewModel>();
        private async Task GoToVisitorsListAsync() => await _navigationService.Navigate<VisitorsListViewModel>();
        private async Task GoToVisitorsListFullInfoAsync() => await _navigationService.Navigate<VisitorsListFullInfoViewModel>();
        private async Task GoToLoginAsync() => await _navigationService.Navigate<LoginViewModel>();
    }
}
