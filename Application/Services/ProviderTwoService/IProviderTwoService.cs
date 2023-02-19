using Application.Services.ProviderTwoService.Dtos;

namespace Application.Services.ProviderTwoService
{
    public interface IProviderTwoService
    {
        Task<ProviderTwoSearchResponseDto?> GetRoutesAsync(ProviderTwoSearchRequestDto request, CancellationToken cancellationToken = default);
    }
}