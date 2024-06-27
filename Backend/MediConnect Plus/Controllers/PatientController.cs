using Domain.DTOs.Allergy;
using Domain.DTOs.Appointment;
using Domain.DTOs.Doctor;
using Domain.DTOs.LifestyleFactors;
using Domain.DTOs.Patient;
using Domain.DTOs.Vaccination;
using Domain.IServices;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }



    [HttpGet("BrowseDoctors")]
    public async Task<ActionResult<IEnumerable<DoctorForBrowsingDTO>>> BrowseDoctors(string? location, string? specialty, string? name)
    {
        var doctors = await _patientService.BrowseDoctors(location, specialty, name);

        return Ok(doctors);
    }

    [HttpPost("AddEmergencyContact")]
    public async Task<IActionResult> AddEmergencyContact(string patientUsername, EmergencyContactInfoDTO emergencyContact)
    {
        var result = await _patientService.AddEmergencyContact(patientUsername, emergencyContact);
        if (result)
            return Ok("Emergency contact added successfully");
        return BadRequest("Failed to add emergency contact");
    }

    [HttpPost("RequestAppointment")]
    public async Task<IActionResult> RequestAppointment(string patientUsername, string doctorUsername, DateTime appointmentDate)
    {
        var result = await _patientService.RequestAppointment(patientUsername, doctorUsername, appointmentDate);
        if (result)
            return Ok("Appointment requested successfully");
        return BadRequest("Failed to request appointment");
    }

    [HttpPost("RequestSecondOpinion")]
    public async Task<ActionResult<DoctorForOutputDTO>> RequestSecondOpinion(string patientUsername, string doctorUsername, string caseDescription)
    {
        var doctor = await _patientService.RequestSecondOpinion(patientUsername, doctorUsername, caseDescription);
        if (doctor != null)
            return Ok(doctor);
        return BadRequest("Failed to request second opinion");
    }

    [HttpGet("ViewPastAppointments")]
    public async Task<ActionResult<IEnumerable<AppointmentDTO>>> ViewPastAppointments(string patientUsername)
    {
        var appointments = await _patientService.ViewPastAppointments(patientUsername);
        if (appointments != null && appointments.Any())
            return Ok(appointments);
        return BadRequest("No past appointments found");
    }

    [HttpGet("ViewUpcomingAppointments")]
    public async Task<ActionResult<IEnumerable<AppointmentDTO>>> ViewUpcomingAppointments(string patientUsername)
    {
        var appointments = await _patientService.ViewUpcomingAppointments(patientUsername);
        if (appointments != null && appointments.Any())
            return Ok(appointments);
        return BadRequest("No upcoming appointments found");
    }

    [HttpPost("AddAllergy")]
    public async Task<ActionResult<AllergyForOutputDTO>> AddAllergy(string patientUsername, AllergyDTO allergy)
    {
        var result = await _patientService.AddAllergy(patientUsername, allergy);
        if (result != null)
            return Ok(result);
        return BadRequest("Failed to add allergy");
    }

    [HttpDelete("DeleteAllergy/{allergyId}")]
    public async Task<IActionResult> DeleteAllergy(int allergyId)
    {
        var result = await _patientService.DeleteAllergy(allergyId);
        if (result)
            return Ok("Allergy deleted successfully");
        return BadRequest("Failed to delete allergy");
    }

    [HttpPut("UpdateAllergy/{allergyId}")]
    public async Task<ActionResult<AllergyForOutputDTO>> UpdateAllergy(int allergyId, AllergyDTO updatedAllergy)
    {
        var allergy = await _patientService.UpdateAllergy(allergyId, updatedAllergy);
        if (allergy != null)
            return Ok(allergy);
        return BadRequest("Failed to update allergy");
    }

    [HttpGet("BrowseAllergies")]
    public async Task<ActionResult<IEnumerable<AllergyForOutputDTO>>> BrowseAllergies(string patientUsername)
    {
        var allergies = await _patientService.BrowseAllargies(patientUsername);
        if (allergies != null && allergies.Any())
            return Ok(allergies);
        return BadRequest("No allergies found");
    }
    [HttpGet("BrowseLifeStyleFactors")]
    public async Task<ActionResult<LifestyleFactorsDTO>> GetLifeStyleFactors(string patientUsername)
    {
        var LifestyleFactors = await _patientService.GetLifeStyleFactors(patientUsername);
        if (LifestyleFactors != null)
            return Ok(LifestyleFactors);
        return BadRequest("No Patient found");

    }
    [HttpPost("BrowseLifeStyleFactors")]
    public async Task<ActionResult<LifestyleFactorsDTO>> AddLifeStyleFactors(string patientUsername,LifestyleFactorsDTO lifeFactors)
    {
        var LifestyleFactors = await _patientService.AddLifeStyleFactors(patientUsername, lifeFactors);
        if (LifestyleFactors != null)
            return Ok(LifestyleFactors);
        return BadRequest("No Patient found");

    }
    [HttpPut("BrowseLifeStyleFactors")]
    public async Task<ActionResult<LifestyleFactorsDTO>> UpdateLifeStyleFactors(string patientUsername, LifestyleFactorsForUpdatingDTO lifeFactors)
    {
        var LifestyleFactors = await _patientService.UpdateLifeStyleFactors(patientUsername, lifeFactors);
        if (LifestyleFactors != null)
            return Ok(LifestyleFactors);
        return BadRequest("No Patient found");
    }
    
    

    [HttpGet("ViewFullDetailsPatient")]
    public async Task<ActionResult<PatientFullDTO>> ViewFullDetailsPatient(string patientUsername)
    {
        var patientDetails = await _patientService.ViewFullDetailsPatient(patientUsername);
        if (patientDetails != null)
            return Ok(patientDetails);
        return BadRequest("Patient details not found");
    }
}


