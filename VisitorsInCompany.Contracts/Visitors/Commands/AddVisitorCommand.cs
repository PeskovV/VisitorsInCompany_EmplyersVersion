using MediatR;

namespace VisitorsInCompany.Contracts.Visitors.Commands
{
    public class AddVisitorCommand : IRequest
    {
        public AddVisitorCommand(VisitorDto visitorDto)
        {
            VisitorDto = visitorDto;
        }

        public VisitorDto VisitorDto { get; set; }
    }
}