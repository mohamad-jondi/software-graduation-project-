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
    [HttpGet("Pateint/{PateintUsername}")]
    public async Task<ActionResult<IEnumerable<CaseDTO>>> GetCasesBypatient(string PateintUsername)
    {
        var cases = await _caseService.GetCasesAsync(PateintUsername);
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


    [HttpPut("{caseid}")]
    public async Task<ActionResult<CaseDTO>> EditCaseInfo(int caseid, [FromBody] CaseForUpdating caseDTO)
    {
        var newCase = await _caseService.UpdateCaseAsync(caseid,caseDTO);
        if (newCase != null) return Ok(newCase);
        return BadRequest("Failed to update the case.");
    }
    [HttpPost("case/{caseId}/add-drug")]
    public async Task<IActionResult> AddDrugToCase(int caseId, [FromBody] DrugDTO drug)
    {
        var result = await _caseService.AddDrugToCaseAsync(caseId, drug);
        if (result) return Ok();
        return BadRequest("Failed to add drug to case.");
    }

    [HttpPost("case/{caseId}/add-symptom")]
    public async Task<IActionResult> AddSymptomToCase(int caseId, [FromBody] SymptomsDTO symptom)
    {
        var result = await _caseService.AddSymptomToCaseAsync(caseId, symptom);
        if (result) return Ok();
        return BadRequest("Failed to add symptom to case.");
    }


    [HttpPost("case/{caseId}/add-Document")]
    public async Task<ActionResult<RelatedDocumentDTO>> AddSymptomToCase(int caseId, [FromBody] ImageUploadRequestDTO pic)
    {
        var result = await _caseService.AddDocumetAsync(caseId, pic);
        if (result != null) return Ok(result);
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

