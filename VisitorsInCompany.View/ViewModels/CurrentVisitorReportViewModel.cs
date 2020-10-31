
namespace VisitorsInCompany.ViewModels
{
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using VisitorsInCompany.Helpers;
    using VisitorsInCompany.Interfaces;

    public class CurrentVisitorReportViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IRepository _repo;

        private ObservableCollection<VisitorViewModel> _visitors;
        private VisitorViewModel _currentVisitor;

        public IMvxAsyncCommand GoToMainScreenCommand => new MvxAsyncCommand(GoToMainScreenAsync);
        public IMvxAsyncCommand GoToReportViewCommand => new MvxAsyncCommand(GoToReportViewAsync);
        public IMvxCommand ExitVisitorCommand => new MvxCommand(ExitVisitorAsync);

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

        public CurrentVisitorReportViewModel(IMvxNavigationService navigationService, IRepository repo)
        {
            _navigationService = navigationService;
            _repo = repo;
        }

        public override async Task Initialize()
        {
            FillInVisitors();
            await base.Initialize();
        }

        public void SortCollection(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            FillInVisitors();

            var visitors = Visitors.Where(v => v.FullName.Contains(text, StringComparison.OrdinalIgnoreCase));
            Visitors = new ObservableCollection<VisitorViewModel>();
        }

        private void FillInVisitors()
        {
            List<VisitorViewModel> viewModels = new List<VisitorViewModel>();
            foreach (var item in _repo.GetNotExitVisitors())
                viewModels.Add(new VisitorViewModel(item));

            Visitors = new ObservableCollection<VisitorViewModel>(viewModels);
        }

        private void ExitVisitorAsync()
        {
            var result = MessageBox.Show("Установить факт выхода посетителя?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                CurrentVisitor.Visitor.ExitTime = DateTime.Now.ToString();
                _repo.RemoveFromOrganization(CurrentVisitor.Visitor);
                Visitors.Remove(CurrentVisitor);
            }
            //await _navigationService.Navigate<MainScreenViewModel>();
        }

        private async Task GoToMainScreenAsync() =>
            await _navigationService.Navigate<MainScreenViewModel>();

        private async Task GoToReportViewAsync() =>
            await _navigationService.Navigate<ReportsViewModel>();
    }
}
