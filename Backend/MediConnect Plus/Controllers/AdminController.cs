using Data.Models;
using Domain.DTOs.Doctor;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("unverified-doctors")]
    public async Task<ActionResult<IEnumerable<DoctorWithCredinttialsDTO>>> GetUnverifiedDoctors()
    {
        var doctors = await _adminService.GetUnverifiedDoctorsAsync();
        return Ok(doctors);
    }

    [HttpPost("verify-doctor/{username}")]
    public async Task<ActionResult> VerifyDoctor(string username)
    {
        try
        {
            await _adminService.VerifyDoctorAsync(username);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
