using MediatR;

namespace Application.Routes.Queries.GetRoutes
{
    public class GetRoutesQuery : IRequest<SearchResponseDto>
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
}
