using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VisitorsInCompany.Contracts.Visitors.Queries;

namespace VisitorsInCompany.View.ViewModels
{
    public class VisitorsListFullInfoViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private VisitorViewModel _currentVisitor;

        public IMvxAsyncCommand ToMainScreenCommand => new MvxAsyncCommand(ToMainScreenAsync);

        public VisitorViewModel CurrentVisitor
        {
            get => _currentVisitor;
            set
            {
                _currentVisitor = value;
                RaisePropertyChanged(() => CurrentVisitor);
            }
        }

        public VisitorsListFullInfoViewModel(IMvxNavigationService navigationService, IMediator mediator, IMapper mapper)
        {
            _navigationService = navigationService;
            _mediator = mediator;
            _mapper = mapper;
        }

        public ObservableCollection<VisitorViewModel> Visitors { get; private set; }

        public override async Task Initialize()
        {
            var visitors = _mapper.Map<IEnumerable<VisitorViewModel>>(await _mediator.Send(new GetNotExitVisitorsQuery()));
            Visitors = new ObservableCollection<VisitorViewModel>(visitors);
            await base.Initialize();
        }

        private async Task ToMainScreenAsync() => await _navigationService.Navigate<MainScreenViewModel>(); 
    }
}
