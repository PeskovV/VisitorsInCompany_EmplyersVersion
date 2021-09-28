using System;
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
    public class GetAllVisitorsQueryHandler : IRequestHandler<GetAllVisitorsQuery, IEnumerable<VisitorDto>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllVisitorsQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public Task<IEnumerable<VisitorDto>> Handle(GetAllVisitorsQuery request, CancellationToken cancellationToken)
        {
            var visitors = _context.Visitors
                .Where(v => DateTime.Parse(v.EntryTime).Date >= request.FirstDate && DateTime.Parse(v.EntryTime).Date <= request.SecondDate)
                .ToList();
            var result = _mapper.Map<IEnumerable<VisitorDto>>(visitors);
            return Task.FromResult(result);
        }
    }
}