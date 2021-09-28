using MediatR;

namespace VisitorsInCompany.Contracts.Visitors.Queries
{
    public class GetVisitorQuery : IRequest<VisitorDto>
    {
        public VisitorDto VisitorDto { get; }

        public GetVisitorQuery(VisitorDto visitorDto)
        {
            VisitorDto = visitorDto;
        }
    }
}