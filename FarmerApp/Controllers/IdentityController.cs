using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var authResponse = _identityService.Login(loginRequest);

            return Ok(authResponse);
        }

        [HttpPost("Refresh")]
        public IActionResult Refresh(RefreshTokenRequest refreshTokenRequest)
        {
            var authResponse = _identityService.Refresh(refreshTokenRequest);

            return Ok(authResponse);
        }

        [Authorize]
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "NameIdentifier").Value);

            _identityService.Logout(userId);

            return Ok();
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "NameIdentifier").Value);

            _identityService.ChangePassword(userId, changePasswordRequest);

            return Ok();
        }
    }
}
