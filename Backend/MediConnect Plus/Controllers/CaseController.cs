using Domain.DTOs;
using Domain.DTOs.Cases;
using Domain.DTOs.Symptoms;
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
    public async Task<ActionResult<CaseDTO>> AddCase([FromBody] CaseForCreationDTO caseDTO)
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
    public async Task<ActionResult<IEnumerable<DocumentDTO>>> ViewDocuments(int caseId)
    {
        var result = await _caseService.ViewDocuments(caseId);
        return Ok(result);
    }

    [HttpPost("case/{caseId}/documents")]
    public async Task<ActionResult<bool>> UploadDocuments(int caseId, [FromBody] DocumentDTO document)
    {
        var result = await _caseService.UploadDocument(caseId, document);
        if (result != null) return Ok(result);
        return BadRequest();
    }

    [HttpDelete("documents/{documentId}")]
    public async Task<IActionResult> DeleteDocuments(int documentId)
    {
        var result = await _caseService.DeleteDocument(documentId);
        if (result) return Ok();
        return BadRequest();
    }

    //[HttpPost("case/{caseId}/assign-nurse")]
    //public async Task<IActionResult> AssignNurse(int caseId, [FromBody] NurseDTO nurse)
    //{
    //    var result = await _caseService.AssignNurseAsync(caseId, nurse);
    //    if (result) return Ok();
    //    return BadRequest("Failed to assign nurse.");
    //}

    [HttpPost("case/{caseId}/add-drug")]
    public async Task<IActionResult> AddDrugToCase(int caseId, [FromBody] DrugDTO drug)
    {
        var result = await _caseService.AddDrugToCaseAsync(caseId, drug);
        if (result) return Ok();
        return BadRequest("Failed to add drug to case.");
    }

    [HttpPost("case/{caseId}/add-note")]
    public async Task<IActionResult> AddNoteToCase(int caseId,string note)
    {
        var result = await _caseService.AddNoteToCaseAsync(caseId, note);
        if (result) return Ok();
        return BadRequest("Failed to add note to case.");
    }

    [HttpPost("case/{caseId}/add-symptom")]
    public async Task<IActionResult> AddSymptomToCase(int caseId, [FromBody] SymptomsDTO symptom)
    {
        var result = await _caseService.AddSymptomToCaseAsync(caseId, symptom);
        if (result) return Ok();
        return BadRequest("Failed to add symptom to case.");
    }

    [HttpPost("case/{caseId}/add-diagnosis")]
    public async Task<IActionResult> AddDiagnosisToCase(int caseId, string diagnosis)
    {
        var result = await _caseService.AddDiagnosisToCaseAsync(caseId, diagnosis);
        if (result) return Ok();
        return BadRequest("Failed to add diagnosis to case.");
    }
}

