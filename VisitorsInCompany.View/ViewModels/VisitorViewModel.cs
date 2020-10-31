
namespace VisitorsInCompany.ViewModels
{
    using MvvmCross.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VisitorsInCompany.Models;

    public class VisitorViewModel : MvxViewModel
    {
        private string _fullName;

        public Visitor Visitor { get; set; }
        public string FullName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_fullName))
                    return ($"{Visitor.LastName} {Visitor.FirstName} {Visitor.Patronymic}").TrimStart();
                return _fullName;
            }
            set
            {
                _fullName = value.TrimStart();
                Visitor.FirstName = GetFirstName(_fullName);
                Visitor.LastName = GetLastName(_fullName);
                Visitor.Patronymic = GetPatronymic(_fullName);
                RaisePropertyChanged(() => FullName);
            }
        }

        public string Organization
        {
            get => Visitor.Organization;
            set
            {
                Visitor.Organization = value;
                RaisePropertyChanged(() => Organization);
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

        public string VisitGoal
        {
            get
            {
                if (string.IsNullOrEmpty(Visitor.VisitGoal))
                    Visitor.VisitGoal = VisitGoals.FirstOrDefault();

                return Visitor.VisitGoal;
            }
            set
            {
                Visitor.VisitGoal = value;
                RaisePropertyChanged(() => VisitGoal);
            }
        }

        public string Attendant
        {
            get => Visitor.Attendant;
            set
            {
                Visitor.Attendant = value;
                RaisePropertyChanged(() => Attendant);
            }
        }

        public string Note
        {
            get => Visitor.Note;
            set
            {
                Visitor.Note = value;
                RaisePropertyChanged(() => Note);
            }
        }

        public VisitorViewModel()
        {
            Visitor = new Visitor();
        }

        public VisitorViewModel(Visitor visitor)
        {
            Visitor = visitor ?? new Visitor();
        }

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
