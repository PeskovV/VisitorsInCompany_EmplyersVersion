using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using MediatR;
using VisitorsInCompany.Contracts.Visitors;
using VisitorsInCompany.Contracts.Visitors.Commands;
using VisitorsInCompany.Contracts.Visitors.Queries;

namespace VisitorsInCompany.View.ViewModels
{
    public class CurrentVisitorReportViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        private ObservableCollection<VisitorViewModel> _visitors;
        private VisitorViewModel _currentVisitor;

        public IMvxAsyncCommand GoToMainScreenCommand => new MvxAsyncCommand(GoToMainScreenAsync);
        public IMvxAsyncCommand GoToReportViewCommand => new MvxAsyncCommand(GoToReportViewAsync);
        public IMvxCommand ExitVisitorCommand => new MvxAsyncCommand(ExitVisitorAsync);

        public ObservableCollection<VisitorViewModel> Visitors
        {
            get => _visitors;
            private set
            {
                _visitors = value;
                RaisePropertyChanged(() => Visitors);
            }
        }

        public VisitorViewModel CurrentVisitor
        {
            get => _currentVisitor;
            set
            {
                _currentVisitor = value;
                RaisePropertyChanged(() => CurrentVisitor);
            }
        }

        public CurrentVisitorReportViewModel(IMvxNavigationService navigationService, IMapper mapper,
            IMediator mediator)
        {
            _navigationService = navigationService;
            _mapper = mapper;
            _mediator = mediator;
        }

        public override async Task Initialize()
        {
            await FillInVisitors();
            await base.Initialize();
        }

        //ToDo посмотреть нужен ли здесь FillInVisitors
        public async Task SortCollection(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            await FillInVisitors();
            
            Visitors = new ObservableCollection<VisitorViewModel>(Visitors.Where(v => v.FullName.Contains(text, StringComparison.OrdinalIgnoreCase)));
        }

        private async Task FillInVisitors()
        {
            var visitors = _mapper.Map<IEnumerable<VisitorViewModel>>(await _mediator.Send(new GetNotExitVisitorsQuery()));
            Visitors = new ObservableCollection<VisitorViewModel>(visitors);
        }

        private async Task ExitVisitorAsync()
        {
            var result = MessageBox.Show("Установить факт выхода посетителя?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                CurrentVisitor.ExitTime = DateTime.Now.ToString();
                var dto = _mapper.Map<VisitorDto>(CurrentVisitor);
                await _mediator.Send(new RemoveVisitorFromOrganizationCommand(dto));
                Visitors.Remove(CurrentVisitor);
            }
        }

        private async Task GoToMainScreenAsync() =>
            await _navigationService.Navigate<MainScreenViewModel>();

        private async Task GoToReportViewAsync() =>
            await _navigationService.Navigate<ReportsViewModel>();
    }
}
