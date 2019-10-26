using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsInCompany.Models;

namespace VisitorsInCompany.Interfaces
{
   public interface IRepository
   {
      void Add(Visitor visitor);

      void RemoveFromOrganization(Visitor visitor);

      void SetExitTime(Visitor visitor, DateTime exitTime);

      bool VerifyExitVisitor(string firstName, string lastName, string organization);

      IEnumerable<Visitor> GetAllVisitors();

      IEnumerable<Visitor> GetNotExitVisitors();

      Visitor GetVisitor(Visitor visitor);
   }
}
