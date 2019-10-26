using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsInCompany.Models;

namespace VisitorsInCompany.ViewModels
{
   public class VisitorViewModel : MvxViewModel
   {
      public VisitorViewModel()
      {
         Visitor = new Visitor();
      }

      public VisitorViewModel(Visitor visitor)
      {
         if (visitor != null)
            Visitor = visitor;
         else
            Visitor = new Visitor();
      }

      public override async Task Initialize()
      {
         await base.Initialize();
      }

      public Visitor Visitor { get; set; }

      private string _fullName;
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

      public IEnumerable<string> VisitGoals => new List<string> { "Business Meeting/Деловая встреча", "Personal Meeting/Личная встреча", "Interview/Собеседование", "Training/Обучение", "Maintenance/Проведение работ" };

      public string VisitGoal
      {
         get
         {
            if(string.IsNullOrEmpty(Visitor.VisitGoal))
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

      private string GetFirstName(string fullName)
      {
         if (fullName.Split(' ').Length > 1)
            return fullName.Split(' ')[1];

         return string.Empty;
      }

      private string GetLastName(string fullName)
      {
         return fullName.Split(' ')[0];
      }

      private string GetPatronymic(string fullName)
      {
         if (fullName.Split(' ').Length > 2)
            return fullName.Split(' ')[2];

         return string.Empty;
      }
   }
}
