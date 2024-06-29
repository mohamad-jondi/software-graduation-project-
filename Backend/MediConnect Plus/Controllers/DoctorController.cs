// File: C:\Users\moham\Desktop\software\Backend\MediConnect Plus\Controllers\DoctorController.cs
using Domain.DTOs;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs.Cases;
using Data.enums;
using Domain.DTOs.Patient;
using Domain.DTOs.Appointment;
using Data.Models;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly IPictureService _pictureService;

    public DoctorController(IDoctorService doctorService, IPictureService pictureService)
    {
        _doctorService = doctorService;
        _pictureService = pictureService;
    }

    [HttpGet("accepted-appointments/{doctorUsername}")]
    public async Task<ActionResult<IEnumerable<AppointmentForShowDTO>>> GetAcceptedAppointments(string doctorUsername)
    {
        var appointments = await _doctorService.GetAcceptedAppointments(doctorUsername);
        return Ok(appointments);
    }

    [HttpGet("pending-appointments/{doctorUsername}")]
    public async Task<ActionResult<IEnumerable<AppointmentForShowDTO>>> GetPendingAppointments(string doctorUsername)
    {
        var appointments = await _doctorService.GetPendingAppointments(doctorUsername);
        return Ok(appointments);
    }

    [HttpPut("manage-appointment")]
    public async Task<IActionResult> ManageAppointment([FromBody] AppointmentMangmentDTO appointmentDTO)
    {
        var result = await _doctorService.ManageAppointment( appointmentDTO);
        if (result)
            return Ok();
        return BadRequest();
    }

    [HttpGet("availability/{doctorUsername}")]
    public async Task<ActionResult<IEnumerable<DoctorAvaliabilityWithAppointmentsDTO>>> GetDoctorAvailability(string doctorUsername)
    {
        var availabilities = await _doctorService.GetDoctorAvailability(doctorUsername);
        return Ok(availabilities);
    }

    [HttpDelete("availability/{availabilityId}")]
    public async Task<IActionResult> DeleteDoctorAvailability(int availabilityId)
    {
        var result = await _doctorService.DeleteDoctorAvailability(availabilityId);
        if (result)
            return Ok();
        return BadRequest();
    }

    [HttpPost("availability/{doctorUsername}")]
    public async Task<ActionResult<AvaliabilityDTO>> AddDoctorAvailability(string doctorUsername, [FromBody] AvaliabilityDTO availabilityDTO)
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

    [HttpGet("patient-history/{patientUsername}")]
    public async Task<ActionResult<PatientFullDTO>> ViewPatientHistory(string patientUsername)
    {
        var patientHistory = await _doctorService.ViewPatientHistory(patientUsername);
        if (patientHistory != null)
            return Ok(patientHistory);
        return BadRequest();
    }

    [HttpGet("credential/{doctorUsername}")]
    public async Task<ActionResult<IEnumerable<Credential>>> ViewPatientDocuments(string doctorUsername)
    {
        var documents = await _doctorService.GetCredentialsAsync(doctorUsername);
        if (documents != null)
            return Ok(documents);
        return BadRequest();
    }

    [HttpDelete("credential/{id}")]
    public async Task<IActionResult> DeleteDoctorCredential(int id)
    {
        var result = await _doctorService.DeleteCredentialAsync( id);
        if (result)
            return Ok();
        return BadRequest();
    }


    [HttpPost("credential/{doctorUsername}")]
    public async Task<ActionResult> uploadDocument(string doctorUsername, [FromBody] ImageUploadRequestDTO request)
    {
        try
        {
            if (request == null || string.IsNullOrEmpty(request.Base64Image))
            {
                return BadRequest("Invalid image data");
            }

            // Decode the base64 string to byte array
            var imageData = Convert.FromBase64String(request.Base64Image);

            var pictureUrl = await _doctorService.SaveCredintailsAsync(doctorUsername, request.FileName, imageData);
            return Ok(new { Url = pictureUrl });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
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

   
    [HttpPut("snooze-appointments/{doctorUsername}")]
    public async Task<IActionResult> SnoozeAppointments(string doctorUsername, [FromQuery] int minutes)
    {
        var result = await _doctorService.SnoozeDoctorAppointments(doctorUsername, minutes);
        if (result)
            return Ok();
        return BadRequest("Failed to snooze appointments.");
    }

    [HttpPut("move-appointment/{appointmentId}")]
    public async Task<IActionResult> MoveAppointment(int appointmentId)
    {
        var result = await _doctorService.MoveAppointment(appointmentId);
        if (result)
            return Ok();
        return BadRequest("Failed to move the appointment.");
    }
}
