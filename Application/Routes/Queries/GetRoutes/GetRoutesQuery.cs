using Application.Services.ProviderOneService;
using Application.Services.ProviderOneService.Dtos;
using Application.Services.ProviderTwoService;
using Application.Services.ProviderTwoService.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Routes.Queries.GetRoutes
{
    public class GetRoutesQuery : IRequest<SearchResponseDto[]>
    {
        public GetRoutesQuery()
        {
            Filters = new SearchFilterDto();
        }

        // Mandatory
        // Start point of route, e.g. Moscow 
        public string Origin { get; set; }

        // Mandatory
        // End point of route, e.g. Sochi
        public string Destination { get; set; }

        // Mandatory
        // Start date of route
        public DateTime OriginDateTime { get; set; }

        // Optional
        public SearchFilterDto Filters { get; set; }
    }

    internal class GetRoutesQueryHandler : IRequestHandler<GetRoutesQuery, SearchResponseDto[]>
    {
        private readonly IProviderOneService _providerOneService;
        private readonly IProviderTwoService _providerTwoService;
        private readonly IMapper _mapper;

        public GetRoutesQueryHandler(IProviderOneService providerOneService, IProviderTwoService providerTwoService, IMapper mapper)
        {
            _providerOneService = providerOneService;
            _providerTwoService = providerTwoService;
            _mapper = mapper;
        }

        public async Task<SearchResponseDto[]> Handle(GetRoutesQuery query, CancellationToken cancellationToken)
        {
            var providerOneSearchRequest = _mapper.Map<ProviderOneSearchRequestDto>(query);
            var routes1 = await _providerOneService.GetRoutesAsync(providerOneSearchRequest, cancellationToken);

            var providerTwoSearchRequest = _mapper.Map<ProviderTwoSearchRequestDto>(query);
            var routes2 = await _providerTwoService.GetRoutesAsync(providerTwoSearchRequest, cancellationToken);

            return null;
        }
    }
}
