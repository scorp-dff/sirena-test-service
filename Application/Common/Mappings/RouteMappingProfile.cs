using Application.Routes.Queries.GetRoutes;
using Application.Services.ProviderOneService.Dtos;
using Application.Services.ProviderTwoService.Dtos;
using AutoMapper;

namespace Application.Common.Mappings
{
    public class RouteMappingProfile : Profile
    {
        public RouteMappingProfile()
        {
            CreateMap<GetRoutesQuery, ProviderOneSearchRequestDto>()    
                .ForMember(m => m.From, opt => opt.MapFrom(m => m.Origin))
                .ForMember(m => m.To, opt => opt.MapFrom(m => m.Destination))
                .ForMember(m => m.DateFrom, opt => opt.MapFrom(m => m.OriginDateTime))
                .ForMember(m => m.DateTo, opt => opt.MapFrom(m => m.Filters.DestinationDateTime))
                .ForMember(m => m.MaxPrice, opt => opt.MapFrom(m => m.Filters.MaxPrice));

            CreateMap<GetRoutesQuery, ProviderTwoSearchRequestDto>()
                .ForMember(m => m.Departure, opt => opt.MapFrom(m => m.Origin))
                .ForMember(m => m.Arrival, opt => opt.MapFrom(m => m.Destination))
                .ForMember(m => m.DepartureDate, opt => opt.MapFrom(m => m.OriginDateTime))                
                .ForMember(m => m.MinTimeLimit, opt => opt.MapFrom(m => m.Filters.MinTimeLimit));
        }
    }
}
