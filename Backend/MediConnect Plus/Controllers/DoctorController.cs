﻿// File: C:\Users\moham\Desktop\software\Backend\MediConnect Plus\Controllers\DoctorController.cs
using Domain.DTOs;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs.Cases;
using Data.enums;
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
        var appointments = await _doctorService.GetAcceptedAppointments(doctorUsername);
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
