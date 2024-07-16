using HR_LeaveManagement.Application.Contracts.Identity;
using HR_LeaveManagement.Application.Models.IdentityModels;
using Microsoft.AspNetCore.Mvc;

namespace HR_LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest authRequest)
        {
            return Ok(await _authService.Login(authRequest));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest registrationRequest)
        {
            return Ok(await _authService.Register(registrationRequest));
        }
    }
}
