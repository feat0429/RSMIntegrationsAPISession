namespace RSMEnterpriseIntegrationsAPI.Application.DTOs.Login
{
    public record AuthorizationResponseDto
    (
        string Token,
        int ExpirationMinutes
    );
}
