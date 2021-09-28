using MediatR;

namespace VisitorsInCompany.Contracts.Visitors.Commands
{
    public class RemoveVisitorFromOrganizationCommand : IRequest
    {
        public RemoveVisitorFromOrganizationCommand(VisitorDto visitorDto)
        {
            VisitorDto = visitorDto;
        }

        public VisitorDto VisitorDto { get; set; }
    }
}