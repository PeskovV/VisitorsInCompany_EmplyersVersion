using System;
using System.Collections.Generic;
using MediatR;

namespace VisitorsInCompany.Contracts.Visitors.Queries
{
    public class GetAllVisitorsQuery : IRequest<IEnumerable<VisitorDto>>
    {
        public GetAllVisitorsQuery(DateTime firstDate, DateTime secondDate)
        {
            FirstDate = firstDate;
            SecondDate = secondDate;
        }
        
        public DateTime FirstDate { get; set; }
        public DateTime SecondDate { get; set; }
    }
}