using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using VisitorsInCompany.Contracts.Visitors.Commands;
using VisitorsInCompany.DAL.EF;
using VisitorsInCompany.Model.Models;

namespace VisitorsInCompany.Logic.Visitors.Commands
{
    public class RemoveVisitorFromOrganizationCommandHandler : IRequestHandler<RemoveVisitorFromOrganizationCommand>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RemoveVisitorFromOrganizationCommandHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public Task<Unit> Handle(RemoveVisitorFromOrganizationCommand request, CancellationToken cancellationToken)
        {
            var visitor = _mapper.Map<Visitor>(request.VisitorDto);
            var model = _context.Visitors.SingleOrDefault(v => v.Id == visitor.Id);

            if (model == null)
                return Unit.Task;

            model.ExitTime = visitor.ExitTime;

            _context.Visitors.Update(model);
            _context.SaveChanges();
            return Unit.Task;
        }
    }
}