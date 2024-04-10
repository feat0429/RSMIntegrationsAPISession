
namespace RSMEnterpriseIntegrationsAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.Login;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Authorization;

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthorizationService _service;

        public LoginController(IConfiguration configuration, IAuthorizationService service)
        {
            _configuration = configuration;
            _service = service;
        }

        [HttpPost("token")]
        public IActionResult GenerateToken(AuthorizationRequestDto requestDto)
        {
            var token = _service.GetToken(requestDto);

            return Ok(token);
        }
    }
}
