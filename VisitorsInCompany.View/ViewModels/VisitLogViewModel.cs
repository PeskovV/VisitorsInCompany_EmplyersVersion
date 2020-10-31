
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

    public class VisitLogViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IRepository _repo;

        private ObservableCollection<VisitorViewModel> _visitors;
        private VisitorViewModel _currentVisitor;
        private DateTime _firstDate = DateTime.Now;
        private DateTime _secondDate = DateTime.Now;
        private bool _formedData;

        public IMvxAsyncCommand GoToMainScreenCommand => new MvxAsyncCommand(GoToMainScreenAsync);
        public IMvxAsyncCommand GoToReportViewCommand => new MvxAsyncCommand(GoToReportViewAsync);
        public IMvxCommand ExitVisitorCommand => new MvxCommand(ExitVisitorAsync);
        public IMvxCommand FormDataCommand => new MvxCommand(FormDataAsync);

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

        public VisitLogViewModel(IMvxNavigationService navigationService, IRepository repo)
        {
            _navigationService = navigationService;
            _repo = repo;
        }

        public override async Task Initialize() => await base.Initialize();

        public void SortCollection(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            FillInVisitors();
            var visitors = Visitors.Where(v => v.FullName.Contains(text, StringComparison.OrdinalIgnoreCase));
            Visitors = new ObservableCollection<VisitorViewModel>(visitors);
        }

        private void FillInVisitors()
        {
            List<VisitorViewModel> viewModels = new List<VisitorViewModel>();

            var visitors = _repo.GetAllVisitors().Where(v => (DateTime.Parse(v.EntryTime).Date >= FirstDate) && (DateTime.Parse(v.EntryTime).Date <= SecondDate));
            foreach (var visitor in visitors)
                viewModels.Add(new VisitorViewModel(visitor));

            Visitors = new ObservableCollection<VisitorViewModel>(viewModels);
        }

        private void ExitVisitorAsync()
        {
            if (!string.IsNullOrWhiteSpace(CurrentVisitor.Visitor.ExitTime))
            {
                MessageBox.Show("Посетитель уже вышел");
                return;
            }

            if (MessageBox.Show("Установить факт выхода посетителя?", "Подтвердите своё действие", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                CurrentVisitor.Visitor.ExitTime = DateTime.Now.ToString();
                _repo.RemoveFromOrganization(CurrentVisitor.Visitor);
            }
            //await _navigationService.Navigate<MainScreenViewModel>();
        }

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

        private async Task GoToMainScreenAsync() => await _navigationService.Navigate<MainScreenViewModel>();
        private async Task GoToReportViewAsync() => await _navigationService.Navigate<ReportsViewModel>();
    }
}
