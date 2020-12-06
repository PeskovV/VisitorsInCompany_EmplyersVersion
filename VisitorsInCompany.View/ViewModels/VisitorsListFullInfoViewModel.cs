
namespace VisitorsInCompany.ViewModels
{
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using VisitorsInCompany.Interfaces;

    public class VisitorsListFullInfoViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IRepository _repo;

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

        public VisitorsListFullInfoViewModel(IMvxNavigationService navigationService, IRepository repo)
        {
            _navigationService = navigationService;
            _repo = repo;
        }

        public ObservableCollection<VisitorViewModel> Visitors { get; private set; }

        public override async Task Initialize()
        {
            List<VisitorViewModel> viewModels = new List<VisitorViewModel>();
            foreach (var item in _repo.GetNotExitVisitors())
                viewModels.Add(new VisitorViewModel(item));

            Visitors = new ObservableCollection<VisitorViewModel>(viewModels);
            await base.Initialize();
        }

        private async Task ToMainScreenAsync() => await _navigationService.Navigate<MainScreenViewModel>(); 
    }
}
