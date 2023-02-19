using Application.Routes.Queries.GetRoutes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SirenaDemoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteSearchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RouteSearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "GetRoutes")]
        public async Task<IEnumerable<SearchResponseDto>> GetRoutes(GetRoutesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
