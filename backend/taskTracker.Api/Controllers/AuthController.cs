using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using taskTracker.Application.Common.Interfaces;
using taskTracker.Application.DTO;

namespace taskTracker.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
  private readonly IAuthService _authService;

  public AuthController(IAuthService authService)
  {
    _authService = authService;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterRequest request)
  {
    try
    {
      await _authService.RegisterAsync(request);
      return Ok(new { message = "User registered successfully" });
    } catch (AuthenticationException ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginRequest request)
  {
    try
    {
      var result = await _authService.LoginAsync(request);
      return Ok(result);
    } catch (AuthenticationException ex)
    {
      return Unauthorized(ex.Message);
    }
  }
}