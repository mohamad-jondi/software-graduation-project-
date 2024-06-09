using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Domain.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.enums;
using Domain.DTOs.Cases;
using Domain.DTOs.Patient;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet("accepted-appointments/{doctorUsername}")]
    public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAcceptedAppointments(string doctorUsername)
    {
        var appointments = await _doctorService.GetAccpetedAppointments(doctorUsername);
        return Ok(appointments);
    }

    [HttpGet("pending-appointments/{doctorUsername}")]
    public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetPendingAppointments(string doctorUsername)
    {
        var appointments = await _doctorService.GetPendingAppointments(doctorUsername);
        return Ok(appointments);
    }

    [HttpPut("manage-appointment/{appointmentId}")]
    public async Task<IActionResult> ManageAppointment(int appointmentId, [FromBody] AppointmentDTO appointmentDTO)
    {
        var result = await _doctorService.ManageAppointment(appointmentId, appointmentDTO);
        if (result)
            return Ok();
        return BadRequest();
    }

    [HttpGet("availability/{doctorUsername}")]
    public async Task<ActionResult<IEnumerable<AvaliabilityDTO>>> GetDoctorAvailability(string doctorUsername)
    {
        var availabilities = await _doctorService.GetDoctorAvailability(doctorUsername);
        return Ok(availabilities);
    }

    [HttpDelete("availability/{availabilityId}")]
    public async Task<IActionResult> DeleteDoctorAvailability(int availabilityId, [FromBody] AvaliabilityDTO availabilityDTO)
    {
        var result = await _doctorService.DeleteDoctorAvailability(availabilityId, availabilityDTO);
        if (result)
            return Ok();
        return BadRequest();
    }

    [HttpPost("availability/{doctorUsername}")]
    public async Task<ActionResult<AvaliabilityDTO>> AddDoctorAvailability(int doctorUsername, [FromBody] AvaliabilityDTO availabilityDTO)
    {
        var availability = await _doctorService.AddDoctorAvailability(doctorUsername, availabilityDTO);
        if (availability != null)
            return Ok(availability);
        return BadRequest();
    }

    [HttpPut("availability/{availabilityId}")]
    public async Task<ActionResult<AvaliabilityDTO>> UpdateDoctorAvailability(int availabilityId, [FromBody] AvaliabilityDTO availabilityDTO)
    {
        var availability = await _doctorService.UpdateDoctorAvailability(availabilityId, availabilityDTO);
        if (availability != null)
            return Ok(availability);
        return BadRequest();
    }

    [HttpPut("availability-range/{availabilityId}")]
    public async Task<ActionResult<IEnumerable<AvaliabilityDTO>>> UpdateRangeDoctorAvailability(int availabilityId, [FromBody] IEnumerable<AvaliabilityDTO> availabilityDTOs)
    {
        var availabilities = await _doctorService.UpdateRangeDoctorAvailability(availabilityId, availabilityDTOs);
        if (availabilities != null)
            return Ok(availabilities);
        return BadRequest();
    }

    [HttpGet("patient-history/{patientUsername}")]
    public async Task<ActionResult<PatientFullDTO>> ViewPatientHistory(string patientUsername)
    {
        var patientHistory = await _doctorService.ViewPatientHistory(patientUsername);
        if (patientHistory != null)
            return Ok(patientHistory);
        return BadRequest();
    }

    [HttpGet("patient-documents/{patientUsername}")]
    public async Task<ActionResult<IEnumerable<DocumentDTO>>> ViewPatientDocuments(string patientUsername)
    {
        var documents = await _doctorService.ViewPatientDocuments(patientUsername);
        if (documents != null)
            return Ok(documents);
        return BadRequest();
    }

    [HttpPut("credential/{doctorUsername}")]
    public async Task<ActionResult<CredentialDTO>> UpdateDoctorCredential(string doctorUsername, [FromBody] CredentialDTO doctorCredential)
    {
        var credential = await _doctorService.UpdateDoctorCredential(doctorUsername, doctorCredential);
        if (credential != null)
            return Ok(credential);
        return BadRequest();
    }

    [HttpDelete("credential/{doctorUsername}")]
    public async Task<IActionResult> DeleteDoctorCredential(string doctorUsername, [FromBody] CredentialDTO doctorCredential)
    {
        var result = await _doctorService.DeleteDoctorCredential(doctorUsername, doctorCredential);
        if (result)
            return Ok();
        return BadRequest();
    }

    [HttpPost("credential/{doctorUsername}")]
    public async Task<ActionResult<CredentialDTO>> AddDoctorCredential(string doctorUsername, [FromBody] CredentialDTO doctorCredential)
    {
        var credential = await _doctorService.AddDoctorCredential(doctorUsername, doctorCredential);
        if (credential != null)
            return Ok(credential);
        return BadRequest();
    }

    [HttpPut("work-type/{doctorUsername}")]
    public async Task<IActionResult> UpdateDoctorTypeOfWork(string doctorUsername, [FromBody] DoctorWorkType specializationDTO)
    {
        var result = await _doctorService.UpdateDoctorTypeOfWork(doctorUsername, specializationDTO);
        if (result)
            return Ok();
        return BadRequest();
    }

    [HttpGet("cases/{doctorUsername}")]
    public async Task<ActionResult<IEnumerable<CaseDTO>>> ViewCases(string doctorUsername)
    {
        var cases = await _doctorService.ViewCases(doctorUsername);
        return Ok(cases);
    }

    [HttpPut("manage-case/{caseId}")]
    public async Task<IActionResult> ManageCase(int caseId, [FromBody] CaseDTO caseDTO)
    {
        var result = await _doctorService.ManageCase(caseId, caseDTO);
        if (result)
            return Ok();
        return BadRequest();
    }

    [HttpGet("tests/{doctorUsername}")]
    public async Task<ActionResult<IEnumerable<TestDTO>>> GetTests(string doctorUsername)
    {
        var tests = await _doctorService.GetTests(doctorUsername);
        return Ok(tests);
    }

    [HttpPost("test/{doctorUsername}")]
    public async Task<ActionResult<TestDTO>> AddTest(string doctorUsername, [FromBody] TestDTO testDTO)
    {
        var test = await _doctorService.AddTest(doctorUsername, testDTO);
        if (test != null)
            return Ok(test);
        return BadRequest();
    }

    [HttpPut("test-status/{testId}")]
    public async Task<IActionResult> UpdateTestStatus(int testId, [FromBody] TestStatus status)
    {
        var result = await _doctorService.UpdateTestStatus(testId, status);
        if (result)
            return Ok();
        return BadRequest();
    }

    [HttpDelete("test/{testId}")]
    public async Task<IActionResult> DeleteTest(int testId)
    {
        var result = await _doctorService.DeleteTest(testId);
        if (result)
            return Ok();
        return BadRequest();
    }

    [HttpGet("surgeries/{doctorUsername}")]
    public async Task<ActionResult<IEnumerable<SurgeryDTO>>> ViewSurgeries(string doctorUsername)
    {
        var surgeries = await _doctorService.ViewSurgeries(doctorUsername);
        return Ok(surgeries);
    }

    [HttpPost("surgery/{doctorUsername}")]
    public async Task<IActionResult> ScheduleSurgery(string doctorUsername, [FromBody] SurgeryDTO surgeryDTO)
    {
        var result = await _doctorService.ScheduleSurgery(doctorUsername, surgeryDTO);
        if (result)
            return Ok();
        return BadRequest();
    }

    [HttpPut("surgery/{surgeryId}")]
    public async Task<IActionResult> UpdateSurgery(int surgeryId, [FromBody] SurgeryDTO surgeryDTO)
    {
        var result = await _doctorService.UpdateSurgery(surgeryId, surgeryDTO);
        if (result)
            return Ok();
        return BadRequest();
    }

    [HttpDelete("surgery/{surgeryId}")]
    public async Task<IActionResult> CancelSurgery(int surgeryId)
    {
        var result = await _doctorService.CancelSurgery(surgeryId);
        if (result)
            return Ok();
        return BadRequest();
    }

    [HttpGet("documents/{doctorUsername}")]
    public async Task<ActionResult<IEnumerable<DocumentDTO>>> ViewDocuments(string doctorUsername)
    {
        var documents = await _doctorService.ViewDocuments(doctorUsername);
        return Ok(documents);
    }

    [HttpPost("document/{doctorUsername}")]
    public async Task<ActionResult<DocumentDTO>> UploadDocument(string doctorUsername, [FromBody] DocumentDTO documentDTO)
    {
        var document = await _doctorService.UploadDocument(doctorUsername, documentDTO);
        if (document != null)
            return Ok(document);
        return BadRequest();
    }

    [HttpDelete("document/{documentId}")]
    public async Task<IActionResult> DeleteDocument(int documentId)
    {
        var result = await _doctorService.DeleteDocument(documentId);
        if (result)
            return Ok();
        return BadRequest();
    }
}
