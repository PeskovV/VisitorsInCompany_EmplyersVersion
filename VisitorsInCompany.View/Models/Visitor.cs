using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsInCompany.Models
{
    public class Visitor : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _firstName;
        private string _lastName;
        private string _patronymic;
        private string _organization;
        private string _visitGoal;
        private string _attendant;
        private string _note;
        private string _entryTime;
        private string _exitTime;

        public int Id { get; set; }

        [Required]
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstName)));
            }
        }

        [Required]
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastName)));
            }
        }


        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Patronymic)));
            }
        }

        [Required]
        public string Organization
        {
            get => _organization;
            set
            {
                _organization = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Organization)));
            }
        }

        [Required]
        public string VisitGoal
        {
            get => _visitGoal;
            set
            {
                _visitGoal = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VisitGoal)));
            }
        }

        [Required]
        public string Attendant
        {
            get => _attendant;
            set
            {
                _attendant = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Attendant)));
            }
        }

        public string Note
        {
            get => _note;
            set
            {
                _note = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Note)));
            }
        }

        [Required]
        public string EntryTime
        {
            get => _entryTime;
            set
            {
                _entryTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EntryTime)));
            }
        }

        public string ExitTime
        {
            get => _exitTime;
            set
            {
                _exitTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ExitTime)));
            }
        }
    }
}
