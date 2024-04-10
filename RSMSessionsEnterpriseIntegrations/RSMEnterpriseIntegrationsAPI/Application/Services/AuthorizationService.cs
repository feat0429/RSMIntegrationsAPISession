using Microsoft.IdentityModel.Tokens;
using RSMEnterpriseIntegrationsAPI.Application.DTOs.Login;
using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        //Mock credentials are used for authentication, because no
        //more models are allowed. Otherwise, I would have retrieved 
        //credentials from database tables.
        private readonly IConfiguration _configuration;
        private const int _expirationMinutes = 120;

        public AuthorizationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly string _username = "test";
        private readonly string _password = "12345";

        public AuthorizationResponseDto GetToken(AuthorizationRequestDto authorizationDto)
        {
            if (_username != authorizationDto.Username
                || _password != authorizationDto.Password)
                throw new AuthenticationException("Authentication failed. Please check your username and password.");

            var token = GenerateToken(authorizationDto.Username);

            return new AuthorizationResponseDto(token, _expirationMinutes);
        }

        private string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var tokenCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));

            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(_expirationMinutes),
                SigningCredentials = tokenCredentials,
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDes);

            return tokenHandler.WriteToken(token);
        }
    }
}
