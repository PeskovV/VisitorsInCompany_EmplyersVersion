using AutoMapper;
using VisitorsInCompany.Contracts.Visitors;
using VisitorsInCompany.Model.Models;

namespace VisitorsInCompany.Logic.Visitors
{
    public class VisitorsProfile : Profile
    {
        public VisitorsProfile()
        {
            CreateMap<Visitor, VisitorDto>().ReverseMap();
        }
    }
}