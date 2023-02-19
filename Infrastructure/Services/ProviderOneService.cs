using Application.Services.ProviderOneService;
using Application.Services.ProviderOneService.Dtos;
using Infrastructure.Common.Constants;
using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class ProviderOneService : IProviderOneService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProviderOneService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ProviderOneSearchResponseDto?> GetRoutesAsync(ProviderOneSearchRequestDto request, CancellationToken cancellationToken = default)
        {
            var url = $"api/v1/search";
            using HttpClient httpClient = _httpClientFactory.CreateClient(HttpClientNames.ProviderOne);
            using var response = await httpClient.PostAsJsonAsync(url, request, cancellationToken);

            return await response.Content.ReadFromJsonAsync<ProviderOneSearchResponseDto>(cancellationToken: cancellationToken);
        }
    }
}
