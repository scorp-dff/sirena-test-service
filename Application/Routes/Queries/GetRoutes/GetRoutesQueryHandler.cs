using Application.Common.Interfaces;
using Application.Services.ProviderOneService;
using Application.Services.ProviderOneService.Dtos;
using Application.Services.ProviderTwoService;
using Application.Services.ProviderTwoService.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Routes.Queries.GetRoutes
{
    internal class GetRoutesQueryHandler : IRequestHandler<GetRoutesQuery, SearchResponseDto>
    {
        private readonly IProviderOneService _providerOneService;
        private readonly IProviderTwoService _providerTwoService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoutesQueryHandler> _logger;
        private readonly IApplicationDbContext _dbContext;

        public GetRoutesQueryHandler(IProviderOneService providerOneService, IProviderTwoService providerTwoService, IMapper mapper, ILogger<GetRoutesQueryHandler> logger, IApplicationDbContext dbContext)
        {
            _providerOneService = providerOneService;
            _providerTwoService = providerTwoService;
            _mapper = mapper;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<SearchResponseDto> Handle(GetRoutesQuery query, CancellationToken cancellationToken)
        {
            if (query.Filters.OnlyCached ?? false)
            {
                return await GetRoutesFromCacheAsync(query, cancellationToken);
            }

            var allRoutes = new List<RouteDto>();

            try
            {
                var providerOneSearchRequest = _mapper.Map<ProviderOneSearchRequestDto>(query);
                var providerOneSearchResponseDto = await _providerOneService.GetRoutesAsync(providerOneSearchRequest, cancellationToken);
                ArgumentNullException.ThrowIfNull(providerOneSearchResponseDto, nameof(providerOneSearchResponseDto));
                var routeDtos = providerOneSearchResponseDto.Routes?.Select(x => _mapper.Map<RouteDto>(x)).ToArray();
                ArgumentNullException.ThrowIfNull(routeDtos, nameof(routeDtos));
                allRoutes.AddRange(routeDtos);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Error while getting routes from one provider.");
            }

            try
            {
                var providerTwoSearchRequest = _mapper.Map<ProviderTwoSearchRequestDto>(query);
                var providerTwoSearchResponseDto = await _providerTwoService.GetRoutesAsync(providerTwoSearchRequest, cancellationToken);
                ArgumentNullException.ThrowIfNull(providerTwoSearchResponseDto, nameof(providerTwoSearchResponseDto));
                var routeDtos = providerTwoSearchResponseDto.Routes?.Select(x => _mapper.Map<RouteDto>(x)).ToArray();
                ArgumentNullException.ThrowIfNull(routeDtos, nameof(routeDtos));
                allRoutes.AddRange(routeDtos);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Error while getting routes from two provider.");
            }

            try
            {
                var routeEntities = allRoutes.Select(x => _mapper.Map<Route>(x)).ToArray();
                await _dbContext.Routes.AddRangeAsync(routeEntities);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Error when saving routes to database.");
            }

            var response = new SearchResponseDto
            {
                Routes = allRoutes.ToArray()
            };

            return response;
        }

        private async Task<SearchResponseDto> GetRoutesFromCacheAsync(GetRoutesQuery query, CancellationToken cancellationToken)
        {
            var routes = await _dbContext.Routes.AsNoTracking()
                .Where(x => !query.Filters.DestinationDateTime.HasValue || x.DestinationDateTime == query.Filters.DestinationDateTime.Value)
                .Where(x => !query.Filters.MaxPrice.HasValue || x.Price <= query.Filters.MaxPrice.Value)
                .Where(x => !query.Filters.MinTimeLimit.HasValue || x.TimeLimit  >= query.Filters.MinTimeLimit)
                .ProjectTo<RouteDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);

            return new SearchResponseDto
            {
                Routes = routes
            };
        }
    }
}
