﻿using Data.enums;
using Data.Migrations;
using Data.Models;
using Domain.DTOs;
using Domain.DTOs.Appointment;
using Domain.DTOs.Cases;
using Domain.DTOs.Patient;

namespace Domain.IServices
{
    public interface IDoctorService
    {
        Task<IEnumerable<AppointmentForShowDTO>> GetAcceptedAppointments(string doctorUsername);
        Task<IEnumerable<AppointmentForShowDTO>> GetPendingAppointments(string doctorUsername);
        Task<bool> ManageAppointment( AppointmentMangmentDTO appointmentDTO);

        Task<string> SaveCredintailsAsync(string username, string fileName, byte[] imageData);
        Task<bool> DeleteCredentialAsync(int id);
        Task<IEnumerable<Credential>> GetCredentialsAsync(string username);
        Task<DoctorAvaliabilityWithAppointmentsDTO> GetDoctorAvailability(string doctorUsername);
        Task<bool> DeleteDoctorAvailability(int availabilityId);
        Task<AvaliabilityDTO> AddDoctorAvailability(string doctorUsername, AvaliabilityDTO availabilityDTO);
        Task<AvaliabilityDTO> UpdateDoctorAvailability(int availabilityId, AvaliabilityDTO availabilityDTO);
       // Task<IEnumerable<AvaliabilityDTO>> UpdateRangeDoctorAvailability(int availabilityId, IEnumerable<AvaliabilityDTO> availabilityDTO);
        Task<AppointmentReminderDTO> GetUpcomingAppointmentsReminders(string doctorUsername);
        Task<bool> RequestAccessToPatientData(string doctorUsername, string patientUsername);
        Task<PatientFullDTO> ViewPatientHistory(string patientUsername);
        Task<IEnumerable<DocumentDTO>> ViewPatientDocuments(string patientUsername);
        Task<CredentialDTO> UpdateDoctorCredential(string doctorUsername, CredentialDTO doctorCredential);
        Task<bool> DeleteDoctorCredential(string doctorUsername, CredentialDTO doctorCredential);
        Task<CredentialDTO> AddDoctorCredential(string doctorUsername, CredentialDTO doctorCredential);
        Task<bool> UpdateDoctorTypeOfWork(string doctorUsername, DoctorWorkType specializationDTO);

        // Methods for managing cases
        Task<IEnumerable<CaseDTO>> ViewCases(string doctorUsername);
        Task<bool> UpdateCaseInformation(int caseId, CaseDTO caseDTO);

        Task<bool> SnoozeDoctorAppointments(string doctorUsername, int minutes);

        Task<bool> MoveAppointment(int appointmentId);
    }
}
