using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using MediatR;
using VisitorsInCompany.Contracts.Visitors;
using VisitorsInCompany.Contracts.Visitors.Commands;
using VisitorsInCompany.Contracts.Visitors.Queries;
using VisitorsInCompany.Helpers;
using VisitorsInCompany.Model.Models;

namespace VisitorsInCompany.View.ViewModels
{
    public class CreateUserViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

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


        public CreateUserViewModel(IMvxNavigationService navigationService, IMapper mapper, IMediator mediator)
        {
            _navigationService = navigationService;
            _mapper = mapper;
            _mediator = mediator;
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
            if (!VerifyFullData(VisitorVM))
            {
                MessageBox.Show("Заполните все поля\\ Fill all fields", "Warning");
                return;
            }

            if (await _mediator.Send(new VerifyExitVisitorQuery(_mapper.Map<VisitorDto>(VisitorVM))))
            {
                VisitorVM.EntryTime = DateTime.Now.ToString();
                await _mediator.Send(new AddVisitorCommand(_mapper.Map<VisitorDto>(VisitorVM)));
                await GoToMainScreenAsync();
            }
            else
                MessageBox.Show("Просим, вас, обратиться к службе безопасности", "Пользователь в организации");
        }

        private bool VerifyFullData(VisitorViewModel visitor)
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
