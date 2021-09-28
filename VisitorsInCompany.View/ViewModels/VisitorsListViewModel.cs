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
using AutoMapper;
using MediatR;
using VisitorsInCompany.Contracts.Visitors;
using VisitorsInCompany.Contracts.Visitors.Commands;
using VisitorsInCompany.Contracts.Visitors.Queries;
using VisitorsInCompany.Helpers;

namespace VisitorsInCompany.View.ViewModels
{
    public class VisitorsListViewModel : MvxViewModel
    {
        public VisitorsListViewModel(IMvxNavigationService navigationService, IMediator mediator, IMapper mapper)
        {
            _navigationService = navigationService;
            _mediator = mediator;
            _mapper = mapper;
        }
        
        private readonly IMvxNavigationService _navigationService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

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

        public override async Task Initialize()
        {
            var visitors = _mapper.Map<IEnumerable<VisitorViewModel>>(await _mediator.Send(new GetNotExitVisitorsQuery()));
            Visitors = new ObservableCollection<VisitorViewModel>(visitors);
            await base.Initialize();
        }

        private async Task ExitVisitorAsync()
        {
            var result = MessageBox.Show("Вы уверены?\\Are you shure?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                CurrentVisitor.ExitTime = DateTime.Now.ToString();
                var dto = _mapper.Map<VisitorDto>(CurrentVisitor);
                await _mediator.Send(new RemoveVisitorFromOrganizationCommand(dto));
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
