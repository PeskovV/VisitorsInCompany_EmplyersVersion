using System.Collections.Generic;
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
    public class GetNotExitVisitorsQueryHandler : IRequestHandler<GetNotExitVisitorsQuery, IEnumerable<VisitorDto>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetNotExitVisitorsQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public Task<IEnumerable<VisitorDto>> Handle(GetNotExitVisitorsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Visitors.Where(v => string.IsNullOrWhiteSpace(v.ExitTime)).ToList();
            var result = _mapper.Map<IEnumerable<VisitorDto>>(query);
            return Task.FromResult(result);
        }
    }
}