using AutomatadanParameterization.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomatadanParameterization.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginAutomataService _loginService;

        public LoginController(LoginAutomataService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Precondition
            if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Username dan password harus diisi." });
            }

            var result = _loginService.Login(request.Username, request.Password);

            // Postcondition: Jika login gagal
            if (result.StartsWith("Login gagal"))
            {
                return Unauthorized(new
                {
                    message = result,
                    state = _loginService.GetCurrentState().ToString() // Tetap bisa lihat state meskipun gagal
                });
            }

            return Ok(new
            {
                message = result,
                state = _loginService.GetCurrentState().ToString()
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var result = _loginService.Logout();
            return Ok(new
            {
                message = result,
                state = _loginService.GetCurrentState().ToString()
            });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
