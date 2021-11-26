using MediatR;

namespace VisitorsInCompany.Contracts.Visitors.Queries
{
    public class VerifyExitVisitorQuery : IRequest<bool>
    {
        public VisitorDto VisitorDto { get; }

        public VerifyExitVisitorQuery(VisitorDto visitorDto)
        {
            VisitorDto = visitorDto;
        }
    }
}