using System.Collections.Generic;
using MediatR;

namespace VisitorsInCompany.Contracts.Visitors.Queries
{
    public class GetNotExitVisitorsQuery : IRequest<IEnumerable<VisitorDto>>
    {
        
    }
}