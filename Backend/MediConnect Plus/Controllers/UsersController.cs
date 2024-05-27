using Microsoft.AspNetCore.Mvc;
using Domain.DTOs.Login;
using Domain.IServices;
using Domain.DTOs;

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
    public async Task<ActionResult<UserDTO>> Register(RegisterModelDTO model)
    {
        try
        {
            var registered = await _userService.Register(model);
            if (registered != null )
                return Ok(registered);
            else
                return BadRequest("Credentials already used.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to register user: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<JWTTokensDTO>> Login(LoginDTO model)
    {
        try
        {
            var result = await _userService.Login(model);
            if (result != null)
                return Ok(result);
            else
                return Unauthorized();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to authenticate user: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("recover-password-request")]
    public async Task<IActionResult> RecoverPasswordRequest(RecoverPasswordRequestDTO model)
    {
        try
        {
            var success = await _userService.RecovePasswordRequest(model);
            if (success)
                return Ok("Recovery email sent successfully.");
            else
                return BadRequest("Failed to send recovery email.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to send recovery email: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
    {
        try
        {
            var success = await _userService.ResetPassword(model);
            if (success)
                return Ok("Password reset successfully.");
            else
                return BadRequest("Failed to reset password.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to reset password: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("authenticate-email")]
    public async Task<IActionResult> AuthenticateEmail(AuthinticateEmailDTO model)
    {
        try
        {
            var success = await _userService.AuthinticateEmail(model);
            if (success)
                return Ok("Email authenticated successfully.");
            else
                return BadRequest("Failed to authenticate email.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to authenticate email: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}
