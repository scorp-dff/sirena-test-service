﻿namespace Application.Routes.Queries.GetRoutes
{
    public class SearchResponseDto
    {
        // Mandatory
        // Array of routes
        public RouteDto[] Routes { get; set; }

        // Mandatory
        // The cheapest route
        public decimal MinPrice { get; set; }

        // Mandatory
        // Most expensive route
        public decimal MaxPrice { get; set; }

        // Mandatory
        // The fastest route
        public int MinMinutesRoute { get; set; }

        // Mandatory
        // The longest route
        public int MaxMinutesRoute { get; set; }
    }
}
