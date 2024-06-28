using AutoMapper;
using Data.enums;
using Data.Interfaces;
using Data.Models;
using Domain.DTOs.Appointment;
using Domain.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;

        public AppointmentsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AppointmentDTO> CancelAppointment(int AppointmentID, AppointmentCanceltionDTO cancelApontmentDTO)
        {
            var appointment = await _context.GetRepositories<Appointment>()
                                            .Get()
                                            .FirstOrDefaultAsync(c => c.AppointmentId == AppointmentID);
            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }

            appointment.Status = Data.enums.AppointmentStatus.Canceled;
            appointment.CanceledBy = cancelApontmentDTO.CanceledBy;
            appointment.CanceledReson = cancelApontmentDTO.CanceledReson;

            await _context.GetRepositories<Appointment>().Update(appointment);
            return _mapper.Map<AppointmentDTO>(appointment);
        }

        public async Task<AppointmentDTO> GetAppointmentByID(int AppointmentID)
        {
            var appointment = await _context.GetRepositories<Appointment>()
                                            .Get()
                                            .FirstOrDefaultAsync(c => c.AppointmentId == AppointmentID);
            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }

            return _mapper.Map<AppointmentDTO>(appointment);
        }

        public Task<AppointmentDTO> GetAppointmentByID(string UserName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppointmentDTO>> GetAppointments(string UserName )
        {
            var appointments = await _context.GetRepositories<Appointment>()
                                             .Get()
                                             .Where(c => c.Patient.Username == UserName || c.Doctor.Username == UserName )
                                             .ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentDTO>>(appointments);
        }

        public async Task<AppointmentDTO> ManageAppointment(string UserName, AppointmentMangmentDTO appointment)
        {
            var existingAppointment = await _context.GetRepositories<Appointment>()
                                                    .Get()
                                                    .FirstOrDefaultAsync(c => c.AppointmentId == appointment.appointmentId);
            if (existingAppointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }

            existingAppointment.Status = appointment.AppointmentStatus;

            await _context.GetRepositories<Appointment>().Update(existingAppointment);
            return _mapper.Map<AppointmentDTO>(existingAppointment);
        }

        public async Task<AppointmentDTO> MoveAppointment(string username, int AppointmentID)
        {
            var appointments = await _context.GetRepositories<Appointment>()
                                             .Get()
                                             .Where(a => a.Patient.Username == username && a.Status == AppointmentStatus.Pending)
                                             .OrderBy(a => a.Date)
                                             .ToListAsync();

            var appointment = appointments.FirstOrDefault(a => a.AppointmentId == AppointmentID);
            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }

            var closestAppointment = appointments.FirstOrDefault(a => a.Date > appointment.Date);
            if (closestAppointment != null)
            {
                // Swap times
                var tempDate = appointment.Date;
                appointment.Date = closestAppointment.Date;
                closestAppointment.Date = tempDate;

                await _context.GetRepositories<Appointment>().Update(appointment);
                await _context.GetRepositories<Appointment>().Update(closestAppointment);
            }
            else
            {
                // Push by one hour
                appointment.Date = appointment.Date.AddHours(1);

                var doctorAvailability = await _context.GetRepositories<Avaliability>()
                                                       .Get()
                                                       .Where(a => a.DoctorID == appointment.DoctorId && a.DayOfWeek == appointment.Date.DayOfWeek)
                                                       .FirstOrDefaultAsync();

                if (doctorAvailability != null)
                {
                    var startTime = doctorAvailability.StartHour;
                    var endTime = doctorAvailability.EndHour;

                    if (appointment.Date.TimeOfDay < startTime || appointment.Date.TimeOfDay > endTime)
                    {
                        // Cancel appointment if out of availability
                        appointment.Status = AppointmentStatus.Canceled;
                    }
                }
                else
                {
                    // Cancel appointment if no availability found
                    appointment.Status = AppointmentStatus.Canceled;
                }

                await _context.GetRepositories<Appointment>().Update(appointment);
            }

            return _mapper.Map<AppointmentDTO>(appointment);
        }

        public async Task<IEnumerable<AppointmentDTO>> SnozeDoctorAppointments(string DoctorUserName, string minutes)
        {
            if (!int.TryParse(minutes, out int snoozeMinutes) || snoozeMinutes < 5 || snoozeMinutes > 15)
            {
                throw new ArgumentException("Minutes should be between 5 and 15.");
            }

            var today = DateTime.Today;
            var appointments = await _context.GetRepositories<Appointment>()
                                             .Get()
                                             .Where(a => a.Doctor.Username == DoctorUserName && a.Date.Date == today)
                                             .OrderBy(a => a.Date)
                                             .ToListAsync();

            foreach (var appointment in appointments)
            {
                appointment.Date = appointment.Date.AddMinutes(snoozeMinutes);
                await _context.GetRepositories<Appointment>().Update(appointment);
            }

            return _mapper.Map<IEnumerable<AppointmentDTO>>(appointments);
        }
    }
}
