using Application.Services.ProviderTwoService;
using Application.Services.ProviderTwoService.Dtos;
using Infrastructure.Common.Constants;
using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class ProviderTwoService : IProviderTwoService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProviderTwoService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ProviderTwoSearchResponseDto?> GetRoutesAsync(ProviderTwoSearchRequestDto request, CancellationToken cancellationToken = default)
        {
            var url = $"api/v1/search";
            using HttpClient httpClient = _httpClientFactory.CreateClient(HttpClientNames.ProviderTwo);
            using var response = await httpClient.PostAsJsonAsync(url, request, cancellationToken);

            return await response.Content.ReadFromJsonAsync<ProviderTwoSearchResponseDto>(cancellationToken: cancellationToken);
        }
    }
}
