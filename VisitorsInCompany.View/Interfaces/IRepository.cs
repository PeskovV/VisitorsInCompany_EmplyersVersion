
namespace VisitorsInCompany.Interfaces
{
    using System;
    using System.Collections.Generic;
    using VisitorsInCompany.Models;

    public interface IRepository
    {
        void Add(Visitor visitor);
        void RemoveFromOrganization(Visitor visitor);
        void SetExitTime(Visitor visitor, DateTime exitTime);
        bool VerifyExitVisitor(Visitor visitor);
        IEnumerable<Visitor> GetAllVisitors();
        IEnumerable<Visitor> GetNotExitVisitors();
        Visitor GetVisitor(Visitor visitor);
    }
}
