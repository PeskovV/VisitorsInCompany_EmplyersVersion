using AutoMapper;
using VisitorsInCompany.Contracts.Visitors;
using VisitorsInCompany.View.ViewModels;

namespace VisitorsInCompany.View.Profiles
{
    public class VisitorProfile : Profile
    {
        public VisitorProfile()
        {
            CreateMap<VisitorViewModel, VisitorDto>().ReverseMap();
        }
    }
}