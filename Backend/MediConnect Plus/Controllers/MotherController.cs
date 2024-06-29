using Domain.DTOs.Appointment;
using Domain.DTOs.Child;
using Domain.DTOs.Vaccination;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    [HttpPut("{childId}")]
    public async Task<ActionResult<ChildDTO>> UpdateChildInfo(int childId, [FromBody] ChildForCreationDTO childDTO)
    {
        var updatedChild = await _motherService.UpdateChildInfo(childId, childDTO);
        if (updatedChild != null)
            return Ok(updatedChild);
        return BadRequest("Failed to update child info.");
    }

    [HttpGet("child/{childId}")]
    public async Task<ActionResult<ChildDTO>> GetChildById(int childId)
    {
        var child = await _motherService.GetChildByID(childId);
        if (child != null)
            return Ok(child);
        return NotFound("Child not found.");
    }

    [HttpPost("child/{childId}/vaccination")]
    public async Task<ActionResult<VaccinationDTO>> AddChildVaccination(int childId, [FromBody] VaccinationDTO vaccinationDTO)
    {
        var vaccination = await _motherService.AddChildVacination(childId, vaccinationDTO);
        if (vaccination != null)
            return Ok(vaccination);
        return BadRequest("Failed to add child vaccination.");
    }

    [HttpDelete("vaccination/{vaccinationId}")]
    public async Task<IActionResult> DeleteChildVaccination(int vaccinationId)
    {
        var result = await _motherService.DeleteChildVacination(vaccinationId);
        if (result)
            return Ok();
        return BadRequest("Failed to delete child vaccination.");
    }

   

    [HttpPut("vaccination")]
    public async Task<ActionResult<bool>> ManageChildVaccination([FromBody] VaccinationForUpdatingDTO vaccinationDTO)
    {
        var result = await _motherService.UpdateChildVaccinationAsync(vaccinationDTO);
        if (result)
            return Ok();
        return BadRequest("Failed to manage the vaccination.");
    }


    [HttpGet("{motherUsername}/children")]
    public async Task<ActionResult<IEnumerable<ChildDTO>>> GetChildren(string motherUsername)
    {
        var children = await _motherService.GetChildrenAsync(motherUsername);
        return Ok(children);
    }
}
