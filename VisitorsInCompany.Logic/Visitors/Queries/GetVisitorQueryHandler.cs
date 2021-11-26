using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VisitorsInCompany.Contracts.Visitors;
using VisitorsInCompany.Contracts.Visitors.Queries;
using VisitorsInCompany.DAL.EF;

namespace VisitorsInCompany.Logic.Visitors.Queries
{
    public class GetVisitorQueryHandler : IRequestHandler<GetVisitorQuery, VisitorDto>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetVisitorQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<VisitorDto> Handle(GetVisitorQuery request, CancellationToken cancellationToken)
        {
            var visitor = _context.Visitors.SingleOrDefault(v => v.Id == request.VisitorDto.Id);
            return _mapper.Map<VisitorDto>(_context.Visitors.SingleOrDefault(v => v.Id == visitor.Id));
        }
    }
}