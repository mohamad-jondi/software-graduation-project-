using Domain.DTOs;
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
    [HttpGet("case/{caseId}/documents")]

    public async Task<ActionResult<IEnumerable<DocumentDTO>>> ViewDocuments(int CaseID)
    {
        var result = await _caseService.ViewDocuments(CaseID);
        return Ok(result);
    }
    [HttpPost("case/{caseId}/documents")]

    public async Task<ActionResult<IEnumerable<DocumentDTO>>> UploadDocuments(int CaseID, [FromBody] DocumentDTO document)
    {
        var result = await _caseService.UploadDocument(CaseID, document);
        if (result != null) return Ok(result);
        return BadRequest();

    }
    [HttpDelete("documents/{documentid}")]

    public async Task<ActionResult<IEnumerable<DocumentDTO>>> DeleteDocuments(int documentID)
    {
        var result = await _caseService.DeleteDocument(documentID);
        if (result) return Ok();
        return BadRequest();

    }
}
