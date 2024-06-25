using Data.enums;
using Domain.DTOs.Cases;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/[controller]")]
public class CaseController : ControllerBase
{
    private readonly ICaseService _caseService;

    public CaseController(ICaseService caseService)
    {
        _caseService = caseService;
    }

    [HttpGet("doctor/{doctorUsername}")]
    public async Task<ActionResult<IEnumerable<CaseDTO>>> GetCasesByDoctor(string doctorUsername)
    {
        var cases = await _caseService.GetCasesAsync(doctorUsername);
        if (cases != null)
            return Ok(cases);
        return NotFound("No cases found for the specified doctor.");
    }

    [HttpGet("{caseId}")]
    public async Task<ActionResult<CaseDTO>> GetCaseById(int caseId)
    {
        var caseDTO = await _caseService.GetCaseByIdAsync(caseId);
        if (caseDTO != null)
            return Ok(caseDTO);
        return NotFound("Case not found.");
    }

    [HttpPost]
    public async Task<ActionResult<CaseDTO>> AddCase([FromBody] CaseDTO caseDTO)
    {
        var newCase = await _caseService.AddCaseAsync(caseDTO);
        if (newCase != null)
            return CreatedAtAction(nameof(GetCaseById), new { caseId = newCase.CaseId }, newCase);
        return BadRequest("Failed to add the case.");
    }

    [HttpPut("{caseId}")]
    public async Task<IActionResult> UpdateCase(int caseId, [FromBody] CaseDTO caseDTO)
    {
        var result = await _caseService.UpdateCaseAsync(caseId, caseDTO);
        if (result)
            return Ok();
        return BadRequest("Failed to update the case.");
    }

    [HttpDelete("{caseId}")]
    public async Task<IActionResult> DeleteCase(int caseId)
    {
        var result = await _caseService.DeleteCaseAsync(caseId);
        if (result)
            return Ok();
        return BadRequest("Failed to delete the case.");
    }
    [HttpPut("test-status/{testId}")]
    public async Task<IActionResult> UpdateTestStatus(int testId, [FromBody] TestStatus status)
    {
        var result = await _caseService.UpdateTestStatus(testId, status);
        if (result)
            return Ok();
        return BadRequest();
    }

    [HttpDelete("test/{testId}")]
    public async Task<IActionResult> DeleteTest(int testId)
    {
        var result = await _caseService.DeleteTest(testId);
        if (result)
            return Ok();
        return BadRequest();
    }
}
