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

        public VisitorRepository(AppDbContext context) =>
            _context = context;

        public void Add(Visitor visitor)
        {
            /*_context.Visitors.Add(
                new Visitor { 
                    FirstName = visitor.FirstName, 
                    LastName = visitor.LastName, 
                    Attendant = visitor.Attendant, 
                    Organization = visitor.Organization,
                    VisitGoal = visitor.VisitGoal, 
                    EntryTime = visitor.EntryTime, 
                    ExitTime = string.Empty}); */

            _context.Visitors.Add(visitor);
            _context.SaveChanges();
        }

        public IEnumerable<Visitor> GetAllVisitors() =>
           _context.Visitors.ToArray();

        public IEnumerable<Visitor> GetNotExitVisitors() =>
            _context.Visitors.Where(v => string.IsNullOrWhiteSpace(v.ExitTime)).ToArray();

        public Visitor GetVisitor(Visitor visitor) =>
            _context.Visitors.SingleOrDefault(v => v.Id == visitor.Id);

        public void RemoveFromOrganization(Visitor visitor)
        {
            /*var vis = _context.Visitors.FirstOrDefault(v =>
            (v.FirstName == visitor.FirstName) &&
            (v.LastName == visitor.LastName) &&
            (v.Organization == visitor.Organization));*/
            var vis = _context.Visitors.SingleOrDefault(v => v.Id == visitor.Id);

            if (vis == null)
                return;

            vis.ExitTime = visitor.ExitTime;

            _context.Visitors.Update(vis);
            _context.SaveChanges();
        }

        public void SetExitTime(Visitor visitor, DateTime exitTime) =>
            visitor.ExitTime = exitTime.ToString();

        public bool VerifyExitVisitor(Visitor visitor)
        {
            var item = _context.Visitors.FirstOrDefault(v =>
               (v.FirstName == visitor.FirstName) &&
               (v.LastName == visitor.LastName) &&
               (v.Organization == visitor.Organization));

            if (item == null)
                return true;

            return !string.IsNullOrEmpty(item.ExitTime);
        }
    }
}
