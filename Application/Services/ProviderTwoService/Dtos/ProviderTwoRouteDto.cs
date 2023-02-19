namespace Application.Services.ProviderTwoService.Dtos
{
    public class ProviderTwoRouteDto
    {
        // Mandatory
        // Start point of route
        public ProviderTwoPointDto Departure { get; set; }


        // Mandatory
        // End point of route
        public ProviderTwoPointDto Arrival { get; set; }

        // Mandatory
        // Price of route
        public decimal Price { get; set; }

        // Mandatory
        // Timelimit. After it expires, route became not actual
        public DateTime TimeLimit { get; set; }
    }
}
