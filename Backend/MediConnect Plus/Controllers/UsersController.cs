using Data.Models;
using Domain.DTOs;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModelDTO model)
    {
        try
        {
            await _userService.Register(model);
            return Ok("User registered successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to register user: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<JWTTokens>> Login(LoginDTO model)
    {
        try
        {
            var result = await _userService.Login(model);
            if (result != null)
                return Ok(result.Token);
            else
                return Unauthorized();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to authenticate user: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}
