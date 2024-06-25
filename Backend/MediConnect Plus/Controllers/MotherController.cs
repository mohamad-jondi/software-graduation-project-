using Domain.DTOs;
using Domain.DTOs.Vaccination;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MotherController : ControllerBase
{
    private readonly IMotherService _motherService;

    public MotherController(IMotherService motherService)
    {
        _motherService = motherService;
    }

    [HttpPost("{motherUsername}/child")]
    public async Task<ActionResult<ChildDTO>> AddChild(string motherUsername, [FromBody] ChildDTO childDTO)
    {
        var child = await _motherService.AddChildAsync(motherUsername, childDTO);
        if (child != null)
            return CreatedAtAction(nameof(AddChild), new { id = child.ChildId }, child);
        return BadRequest("Failed to add the child.");
    }

    [HttpPut("child/{childId}/vaccination")]
    public async Task<IActionResult> ManageChildVaccination(int childId, [FromBody] VaccinationDTO vaccinationDTO)
    {
        var result = await _motherService.ManageChildVaccinationAsync(childId, vaccinationDTO);
        if (result)
            return Ok();
        return BadRequest("Failed to manage the vaccination.");
    }

    [HttpPut("child/{childId}/appointment")]
    public async Task<IActionResult> ManageChildAppointment(int childId, [FromBody] AppointmentDTO appointmentDTO)
    {
        var result = await _motherService.ManageChildAppointmentAsync(childId, appointmentDTO);
        if (result)
            return Ok();
        return BadRequest("Failed to manage the appointment.");
    }

    [HttpGet("{motherUsername}/children")]
    public async Task<ActionResult<IEnumerable<ChildDTO>>> GetChildren(string motherUsername)
    {
        var children = await _motherService.GetChildrenAsync(motherUsername);
        return Ok(children);
    }

    [HttpPut("snooze-doctor-appointments/{doctorUsername}")]
    public async Task<IActionResult> SnoozeDoctorAppointments(string doctorUsername, [FromQuery] int minutes)
    {
        var result = await _motherService.SnoozeDoctorAppointmentsAsync(doctorUsername, minutes);
        if (result)
            return Ok();
        return BadRequest("Failed to snooze appointments.");
    }

    [HttpPut("move-appointment/{appointmentId}")]
    public async Task<IActionResult> MoveAppointment(int appointmentId, [FromQuery] int minutes)
    {
        var result = await _motherService.MoveAppointmentAsync(appointmentId, minutes);
        if (result)
            return Ok();
        return BadRequest("Failed to move the appointment.");
    }
}
