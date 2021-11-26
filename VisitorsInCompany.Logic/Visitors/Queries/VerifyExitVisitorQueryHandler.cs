using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VisitorsInCompany.Contracts.Visitors.Queries;
using VisitorsInCompany.DAL.EF;

namespace VisitorsInCompany.Logic.Visitors.Queries
{
    public class VerifyExitVisitorQueryHandler : IRequestHandler<VerifyExitVisitorQuery, bool>
    {
        private readonly AppDbContext _context;

        public VerifyExitVisitorQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> Handle(VerifyExitVisitorQuery request, CancellationToken cancellationToken)
        {
            var visitor = request.VisitorDto;
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