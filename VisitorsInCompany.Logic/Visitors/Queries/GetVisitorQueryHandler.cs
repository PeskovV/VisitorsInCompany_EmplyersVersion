using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VisitorsInCompany.Contracts.Visitors;
using VisitorsInCompany.Contracts.Visitors.Queries;
using VisitorsInCompany.DAL.EF;

namespace VisitorsInCompany.Logic.Visitors.Queries
{
    public class GetVisitorQueryHandler : IRequestHandler<GetVisitorQuery, VisitorDto>
    {
        private readonly AppDbContext _context;

        public GetVisitorQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        
        public Task<VisitorDto> Handle(GetVisitorQuery request, CancellationToken cancellationToken)
        {
            var visitor = _context.Visitors.SingleOrDefault(v => v.Id == request.visitor.Id);
            _context.Visitors.SingleOrDefault(v => v.Id == visitor.Id);
        }
    }
}