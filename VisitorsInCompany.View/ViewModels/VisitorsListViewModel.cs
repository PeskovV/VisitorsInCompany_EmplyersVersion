
namespace VisitorsInCompany.ViewModels
{
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using VisitorsInCompany.Helpers;
    using VisitorsInCompany.Interfaces;

    public class VisitorsListViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IRepository _repo;

        private VisitorViewModel _currentVisitor;
        private bool _isRussian = InputLanguageManager.Current.CurrentInputLanguage.Name == "ru-RU";

        public IMvxAsyncCommand ExitVisitorCommand => new MvxAsyncCommand(ExitVisitorAsync);
        public IMvxAsyncCommand ToMainScreenCommand => new MvxAsyncCommand(ToMainScreenAsync);
        public IMvxCommand ShowKeyboardCommand => new MvxCommand(ShowKeyboard);
        public IMvxCommand ChangeLangCommand => new MvxCommand(ChangeLang);

        public ObservableCollection<VisitorViewModel> Visitors { get; private set; }

        public VisitorViewModel CurrentVisitor
        {
            get => _currentVisitor;
            set
            {
                _currentVisitor = value;
                RaisePropertyChanged(() => CurrentVisitor);
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

        public VisitorsListViewModel(IMvxNavigationService navigationService, IRepository repo)
        {
            _navigationService = navigationService;
            _repo = repo;
        }

        public override async Task Initialize()
        {
            List<VisitorViewModel> viewModels = new List<VisitorViewModel>();
            foreach (var item in _repo.GetNotExitVisitors())
                viewModels.Add(new VisitorViewModel(item));

            Visitors = new ObservableCollection<VisitorViewModel>(viewModels);
            await base.Initialize();
        }

        private async Task ExitVisitorAsync()
        {
            var result = MessageBox.Show("Вы уверены?\\Are you shure?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                CurrentVisitor.Visitor.ExitTime = DateTime.Now.ToString();
                _repo.RemoveFromOrganization(CurrentVisitor.Visitor);
                Visitors.Remove(CurrentVisitor);

                await _navigationService.Navigate<MainScreenViewModel>();
            }
            //await _navigationService.Navigate<MainScreenViewModel>();
        }

        private async Task ToMainScreenAsync() => await _navigationService.Navigate<MainScreenViewModel>();

        private void ShowKeyboard() => OperationSystemInteractiveHelper.StartVirtualKeyboard(); 

        private void ChangeLang()
        {
            switch (InputLanguageManager.Current.CurrentInputLanguage.Name)
            {
                case "ru-RU":
                    InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo("en-US");
                    IsRussian = false;
                    break;
                default:
                    InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo("ru-RU");
                    IsRussian = true;
                    break;
            }
        }
    }
}
