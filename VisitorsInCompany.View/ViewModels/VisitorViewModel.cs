using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorsInCompany.View.ViewModels
{ 
    public class VisitorViewModel : MvxViewModel
    {
        private string _fullName;
        private string _firstName;
        private string _lastName;
        private string _patronymic;
        private string _organization;
        private string _visitGoal;
        private string _attendant;
        private string _note;
        private string _entryTime;
        private string _exitTime;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }
        
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }


        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                RaisePropertyChanged(() => Patronymic);
            }
        }
        
        public string Organization
        {
            get => _organization;
            set
            {
                _organization = value;
                RaisePropertyChanged(() => Organization);
            }
        }
        
        public string VisitGoal
        {
            get
            {
                if (string.IsNullOrEmpty(_visitGoal))
                    _visitGoal = VisitGoals.FirstOrDefault();

                return _visitGoal;
            }
            set
            {
                _visitGoal = value;
                RaisePropertyChanged(() => VisitGoal);
            }
        }

        public string Attendant
        {
            get => _attendant;
            set
            {
                _attendant = value;
                RaisePropertyChanged(() => Attendant);
            }
        }

        public string Note
        {
            get => _note;
            set
            {
                _note = value;
                RaisePropertyChanged(() => Note);
            }
        }
        
        public string EntryTime
        {
            get => _entryTime;
            set
            {
                _entryTime = value;
                RaisePropertyChanged(() => EntryTime);
            }
        }

        public string ExitTime
        {
            get => _exitTime;
            set
            {
                _exitTime = value;
                RaisePropertyChanged(() => ExitTime);
            }
        }
        
        public string FullName
        {
            get => string.IsNullOrWhiteSpace(_fullName) ? $"{LastName} {FirstName} {Patronymic}".TrimStart() : _fullName;
            set
            {
                _fullName = value.TrimStart();
                FirstName = GetFirstName(_fullName);
                LastName = GetLastName(_fullName);
                Patronymic = GetPatronymic(_fullName);
                RaisePropertyChanged(() => FullName);
            }
        }

        public IEnumerable<string> VisitGoals =>
            new List<string> {
                "Business Meeting/Деловая встреча",
                "Personal Meeting/Личная встреча",
                "Interview/Собеседование",
                "Training/Обучение",
                "Maintenance/Проведение работ"
            };

        public override async Task Initialize() =>
            await base.Initialize();

        private string GetFirstName(string fullName) =>
            fullName.Split(' ').Length > 1 ? fullName.Split(' ')[1] : string.Empty;

        private string GetLastName(string fullName) =>
            fullName.Split(' ')[0];

        private string GetPatronymic(string fullName) =>
            fullName.Split(' ').Length > 2 ? fullName.Split(' ')[2] : string.Empty;
    }
}
