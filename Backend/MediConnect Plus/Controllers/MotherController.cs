using Domain.DTOs.Appointment;
using Domain.DTOs.Child;
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
    public async Task<ActionResult<ChildDTO>> AddChild(string motherUsername, [FromBody] ChildForCreationDTO childDTO)
    {
        var child = await _motherService.AddChildAsync(motherUsername, childDTO);
        if (child != null)
            return CreatedAtAction(nameof(AddChild), new { id = child.Id }, child);
        return BadRequest("Failed to add the child.");
    }

    [HttpPut("vaccination")]
    public async Task<ActionResult<bool>> ManageChildVaccination([FromBody] VaccinationForUpdatingDTO vaccinationDTO)
    {
        var result = await _motherService.UpdateChildVaccinationAsync( vaccinationDTO);
        if (result)
            return Ok();
        return BadRequest("Failed to manage the vaccination.");
    }

    [HttpPut("child/{childId}/appointment")]
    public async Task<IActionResult> ManageChildAppointment(int childId, [FromBody] AppointmentCanceltionDTO appointmentDTO)
    {
        var result = await _motherService.CancelChildAppointmentAsync(childId, appointmentDTO);
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

}
