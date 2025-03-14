//using CollabTaskManager.Models.DTOs;
//using CollabTaskManager.Services.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace CollabTaskManager.Controllers
//{
//    [Route("api/auth")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly IAuthService _authService;

//        public AuthController(IAuthService authService)
//        {
//            _authService = authService;
//        }

//        [Authorize(Roles = "Admin")]
//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterDto model)
//        {
//            var result = await _authService.RegisterAsync(model, model.Role);
//            return Ok(new { message = result });
//        }

//        //[HttpPost("register")]
//        //public async Task<IActionResult> Register([FromBody] RegisterDto model)
//        //{
//        //    var result = await _authService.RegisterAsync(model, model.Role);
//        //    return Ok(new { message = result });
//        //}
//        //[Authorize(Roles = "Admin")]
//        //[HttpPost]
//        //public async Task<IActionResult> CreateProject([FromBody] ProjectDto model)
//        //{
//        //    // Only Admins can create projects
//        //}

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginDto model)
//        {
//            var token = await _authService.LoginAsync(model);
//            if (token == "Invalid login attempt") return Unauthorized(new { message = token });

//            return Ok(new { token });
//        }
//    }
//}

using CollabTaskManager.Models.DTOs;
using CollabTaskManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CollabTaskManager.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Authorize(Roles = "Admin")] // Remove if regular users should register too
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _authService.RegisterAsync(model, model.Role);

            if (string.IsNullOrEmpty(result) || result.Contains("failed", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = "Registration failed: " + result });
            }

            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var token = await _authService.LoginAsync(model);

            if (string.IsNullOrEmpty(token) || token == "Invalid login attempt")
            {
                return Unauthorized(new { message = "Invalid login credentials" });
            }

            return Ok(new { token });
        }
    }
}
