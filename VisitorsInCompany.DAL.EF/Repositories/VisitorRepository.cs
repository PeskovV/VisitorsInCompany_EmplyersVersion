using System;
using System.Collections.Generic;
using System.Linq;
using VisitorsInCompany.DAL.EF;
using VisitorsInCompany.Model.Models;
using VisitorsInCompany.Model.Repositories;

namespace VisitorsInCompany.DAL.EF.Repositories
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

        public Visitor GetVisitor(Visitor visitor) =>
            _context.Visitors.SingleOrDefault(v => v.Id == visitor.Id);

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
