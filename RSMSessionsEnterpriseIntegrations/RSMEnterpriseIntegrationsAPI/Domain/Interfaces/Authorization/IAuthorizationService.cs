using RSMEnterpriseIntegrationsAPI.Application.DTOs.Login;

namespace RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Authorization
{
    public interface IAuthorizationService
    {
        AuthorizationResponseDto GetToken(AuthorizationRequestDto authorizationDto);
    }
}
