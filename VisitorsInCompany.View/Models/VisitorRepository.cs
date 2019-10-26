using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsInCompany.Data;
using VisitorsInCompany.Interfaces;

namespace VisitorsInCompany.Models
{
   public class VisitorRepository : IRepository
   {
      private readonly AppDbContext _context;

      public VisitorRepository(AppDbContext context)
      {
          _context = context;
      }

      public void Add(Visitor visitor)
      {
         //_context.Visitors.Add(new Visitor { FirstName = visitor.FirstName, LastName = visitor.LastName, Attendant = visitor.Attendant, Organization = visitor.Organization, VisitGoal = visitor.VisitGoal, EntryTime = visitor.EntryTime, ExitTime = string.Empty});
         _context.Visitors.Add(visitor);
         _context.SaveChanges();
      }

      public IEnumerable<Visitor> GetAllVisitors()
      {
         return _context.Visitors.Select(v => v);
      }

      public IEnumerable<Visitor> GetNotExitVisitors()
      {
         return _context.Visitors.Where(v => string.IsNullOrWhiteSpace(v.ExitTime)).Select(v => v);
      }

      public Visitor GetVisitor(Visitor visitor)
      {
         return null;
      }

      public void RemoveFromOrganization(Visitor visitor)
      {
         var vis = _context.Visitors.Where(v =>
           (v.FirstName == visitor.FirstName) &&
           (v.LastName == visitor.LastName) &&
           (v.Organization == visitor.Organization)).FirstOrDefault<Visitor>();

         if(vis != null)
         {
            vis.ExitTime = visitor.ExitTime;
            _context.Visitors.Update(vis);
            _context.SaveChanges();
         }
      }

      public void SetExitTime(Visitor visitor, DateTime exitTime)
      {

      }

      public bool VerifyExitVisitor(string firstName, string lastName, string organization)
      {
         var visitor = _context.Visitors.Where(v =>
            (v.FirstName == firstName) &&
            (v.LastName == lastName) &&
            (v.Organization == organization)).FirstOrDefault<Visitor>();

         if (visitor != null)
            return !string.IsNullOrEmpty(visitor.ExitTime);
         return true;
      }
   }
}
