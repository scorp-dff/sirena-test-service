using Application.Services.ProviderOneService.Dtos;

namespace Application.Services.ProviderOneService
{
    public interface IProviderOneService
    {
        Task<ProviderOneSearchResponseDto?> GetRoutesAsync(ProviderOneSearchRequestDto request, CancellationToken cancellationToken = default);
    }
}