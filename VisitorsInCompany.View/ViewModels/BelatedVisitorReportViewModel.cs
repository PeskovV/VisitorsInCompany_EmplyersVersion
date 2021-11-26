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
    public class BelatedVisitorReportViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        private ObservableCollection<VisitorViewModel> _visitors;
        private VisitorViewModel _currentVisitor;
        private DateTime _firstDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        private DateTime _secondDate = DateTime.Now;
        private bool _formedData;

        public BelatedVisitorReportViewModel(IMvxNavigationService navigationService, IMapper mapper, IMediator mediator)
        {
            _navigationService = navigationService;
            _mapper = mapper;
            _mediator = mediator;
        }

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

        public DateTime FirstDate
        {
            get => _firstDate;
            set
            {
                _firstDate = value;
                RaisePropertyChanged(() => FirstDate);
            }
        }

        public DateTime SecondDate
        {
            get => _secondDate;
            set
            {
                _secondDate = value;
                RaisePropertyChanged(() => SecondDate);
            }
        }

        public bool FormedData
        {
            get => _formedData;
            set
            {
                _formedData = value;
                RaisePropertyChanged(() => FormedData);
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        private async Task FillInVisitors()
        {
            List<VisitorViewModel> viewModel = new List<VisitorViewModel>();

            var visitors = (await _mediator.Send(new GetNotExitVisitorsQuery()))
                .Where(v => (DateTime.Parse(v.EntryTime).Date >= FirstDate) &&
                            (DateTime.Parse(v.EntryTime).Date <= SecondDate));
            Visitors = new ObservableCollection<VisitorViewModel>(_mapper.Map<IEnumerable<VisitorViewModel>>(visitors));
        }

        public async Task SortCollection(string text)
        {
            text = text.ToLower();
            await FillInVisitors();
            if (!string.IsNullOrWhiteSpace(text))
                Visitors = new ObservableCollection<VisitorViewModel>(Visitors.Where(v => v.FullName.ToLower().Contains(text)));
        }

        public IMvxAsyncCommand GoToMainScreenCommand => 
            new MvxAsyncCommand(GoToMainScreenAsync);

        private async Task GoToMainScreenAsync() =>
            await _navigationService.Navigate<MainScreenViewModel>();

        public IMvxAsyncCommand GoToReportViewCommand => 
            new MvxAsyncCommand(GoToReportViewAsync);

        private async Task GoToReportViewAsync() =>
            await _navigationService.Navigate<ReportsViewModel>();

        public IMvxCommand ExitVisitorCommand => 
            new MvxAsyncCommand(ExitVisitorAsync);

        private async Task ExitVisitorAsync()
        {
            var result = MessageBox.Show("Установить факт выхода посетителя?", "Подтвердите своё действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                CurrentVisitor.ExitTime = DateTime.Now.ToString();
                var dto = _mapper.Map<VisitorDto>(CurrentVisitor);
                await _mediator.Send(new RemoveVisitorFromOrganizationCommand(dto));
                Visitors.Remove(CurrentVisitor);
            }
            //await _navigationService.Navigate<MainScreenViewModel>();
        }

        public IMvxCommand FormDataCommand => 
            new MvxCommand(FormDataAsync);

        private void FormDataAsync()
        {
            if (FirstDate > SecondDate)
                MessageBox.Show("Некорректный формат даты");
            else
            {
                FillInVisitors();
                FormedData = true;
            }
        }
    }
}
