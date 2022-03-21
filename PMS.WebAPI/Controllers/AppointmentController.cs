using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Domain.DTO;
using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailRepository _mailService;
        public AppointmentController(IUnitOfWork unitOfWork, IMailRepository mailService)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Appointment> appointments = _unitOfWork.Appointment.GetAll().Where(x => x.IsActive == true).ToList();

                if (!appointments.Any())
                {
                    return StatusCode(404, "Not found");
                }

                return Ok(GetAppointmrentDTO(appointments));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{appointmentId}")]
        public IActionResult GetById(int appointmentId)
        {
            try
            {
                Appointment appointment = _unitOfWork.Appointment.GetById(appointmentId);
                if (appointment ==  null || !appointment.IsActive)
                {
                    return StatusCode(404, "not found");
                }

                AppointmentDTO appointmentDTO = new AppointmentDTO
                {
                    AppointmentId = appointment.Appointment_Id,
                    Title = appointment.Title,
                    AppointmentDate = appointment.Appointment_Date,
                    AppointmentTime = appointment.Appointment_Time,
                    Reason = appointment.Reason,
                    SchedularId = appointment.Schedular_Id,
                    PhysicianId = appointment.Physician_Id,
                    NurseId = appointment.Nurse_Id,
                    PatientId = appointment.Patient_Id,
                    CreatedOn = appointment.CreatedOn,
                    ModifiedOn = appointment.ModifiedOn,
                    IsActive = appointment.IsActive,
                    Status = appointment.Status,
                    DeclineReason = appointment.DeclineReason
                };

                if (appointment.Patient_Id != null && appointment.Patient_Id != 0)
                {
                    var registration = _unitOfWork.RegistrationRepo.GetById(appointment.Patient_Id ?? 0);
                    appointmentDTO.PatientName = registration.FirstName + " " + registration.LastName;
                }
                
                if (appointment.Physician_Id != null && appointment.Physician_Id != 0)
                {
                    var registration = _unitOfWork.RegistrationRepo.GetById(appointment.Physician_Id ?? 0);
                    appointmentDTO.PhysicianName = registration.FirstName + " " + registration.LastName;
                }

                return Ok(appointmentDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetPhysicianAppointments/{userId}")]
        public IActionResult GetPhysicianAppointments(int userId)
        {
            try
            {
                List<AppointmentListByUser> appointments = _unitOfWork.Appointment.GetAppointmentListByPhysicianId(userId).ToList();

                if (!appointments.Any())
                {
                    return StatusCode(404, "Not found");
                }

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetPatientAppointments/{userId}")]
        public IActionResult GetPatientAppointments(int userId)
        {
            try
            {
                List<AppointmentListByUser> appointments = _unitOfWork.Appointment.GetAppointmentListByPatientId(userId).ToList();

                if (!appointments.Any())
                {
                    return StatusCode(404, "Not found");
                }

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetNurseAppointments/{userId}")]
        public IActionResult GetNurseAppointments(int userId)
        {
            try
            {
                List<AppointmentListByUser> appointments = _unitOfWork.Appointment.GetAppointmentListByNurseId(userId).ToList();

                if (!appointments.Any())
                {
                    return StatusCode(404, "Not found");
                }

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody] AppointmentDTO appointmentDTO)
        {
            try
            {
                if (appointmentDTO == null)
                {
                    return StatusCode(400);
                }

                _unitOfWork.Appointment.Add(GetAppointment(appointmentDTO));
                _unitOfWork.Complete();

                string date = appointmentDTO.AppointmentDate?.ToString("MM/dd/yyyy");
                string time = appointmentDTO.AppointmentTime ?? null;

                bool PatientMail = SendMail(appointmentDTO, string.Format(@"Hello,<br/> Appointment has been scheduled <br/> Date :- {0} <br/> Time Slot :- {1} <br/><br/> Thanks, <br/> PMS Team", date, time)).Result;


                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AppointmentDTO appointmentDTO)
        {
            try
            {
                if (appointmentDTO.AppointmentId != id)
                {
                    return StatusCode(400);
                }

                Appointment appointment = _unitOfWork.Appointment.GetById(id);

                if (appointment == null)
                {
                    return StatusCode(400);
                }

                appointment.Title = appointmentDTO.Title;
                appointment.Patient_Id = appointmentDTO.PatientId;
                appointment.Physician_Id = appointmentDTO.PhysicianId;
                appointment.Reason = appointmentDTO.Reason;
                appointment.Nurse_Id = appointmentDTO.NurseId;
                appointment.Schedular_Id = appointmentDTO.SchedularId;
                appointment.Appointment_Date = appointmentDTO.AppointmentDate;
                appointment.Appointment_Time = appointmentDTO.AppointmentTime;
                appointment.CreatedOn = appointmentDTO.CreatedOn;
                appointment.ModifiedOn = appointmentDTO.ModifiedOn;
                appointment.IsActive = appointmentDTO.IsActive;
                appointment.Status = appointmentDTO.Status;
                appointment.DeclineReason = appointmentDTO.DeclineReason;

                _unitOfWork.Appointment.Update(appointment);
                _unitOfWork.Complete();

                string date = appointmentDTO.AppointmentDate?.ToString("MM/dd/yyyy");
                string time = appointmentDTO.AppointmentTime ?? null;

                bool sendMailResult = SendMail(appointmentDTO, string.Format(@"Hello,<br/> Appointment has been updated <br/> Date :- {0} <br/> Time Slot :- {1} <br/><br/> Thanks, <br/> PMS Team", date, time)).Result;

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Decline")]
        public IActionResult Decline([FromQuery]int id, [FromQuery] string status)
        {
            try
            {
                if (id==0)
                {
                    return StatusCode(400);
                }

                Appointment appointment = _unitOfWork.Appointment.GetById(id);

                if (appointment == null)
                {
                    return StatusCode(400);
                }

                
                appointment.Status = status;
                //appointment.DeclineReason = appointmentDTO.DeclineReason;

                _unitOfWork.Appointment.Update(appointment);
                _unitOfWork.Complete();

                string date = appointment.Appointment_Date?.ToString("MM/dd/yyyy");
                string time = appointment.Appointment_Time ?? null;
                AppointmentDTO appointmentDTO = new AppointmentDTO();
                appointmentDTO.AppointmentDate = appointment.Appointment_Date;
                appointmentDTO.AppointmentId = appointment.Appointment_Id;
                appointmentDTO.AppointmentTime = appointment.Appointment_Time;
                appointmentDTO.CreatedOn = appointment.CreatedOn;
                appointmentDTO.DeclineReason = appointment.DeclineReason;
                appointmentDTO.IsActive = appointment.IsActive;
                appointmentDTO.ModifiedOn = appointment.ModifiedOn;
                appointmentDTO.NurseId = appointment.Nurse_Id;
                appointmentDTO.PatientId = appointment.Patient_Id;
                appointmentDTO.PhysicianId = appointment.Physician_Id;
                appointmentDTO.Reason = appointment.Reason;
                appointmentDTO.SchedularId = appointment.Schedular_Id;
                appointmentDTO.Status = "Declined";
                appointmentDTO.Title = appointment.Title;
               

                //bool sendMailResult = SendMail(appointmentDTO, string.Format(@"Hello,<br/> Appointment has been declined by Physician <br/> Date :- {0} <br/> Time Slot :- {1} <br/><br/> Thanks, <br/> PMS Team", date, time)).Result;

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<AppointmentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Appointment appointment = _unitOfWork.Appointment.GetById(id);
            if (appointment == null)
            {
                return StatusCode(400);
            }
            
            appointment.IsActive = false;
            _unitOfWork.Appointment.Update(appointment);
            _unitOfWork.Complete();

            AppointmentDTO appointmentDTO = new AppointmentDTO
            {
                AppointmentId = appointment.Appointment_Id,
                Title = appointment.Title,
                AppointmentDate = appointment.Appointment_Date,
                AppointmentTime = appointment.Appointment_Time,
                Reason = appointment.Reason,
                SchedularId = appointment.Schedular_Id,
                PhysicianId = appointment.Physician_Id,
                NurseId = appointment.Nurse_Id,
                PatientId = appointment.Patient_Id,
                CreatedOn = appointment.CreatedOn,
                ModifiedOn = appointment.ModifiedOn,
                IsActive = appointment.IsActive,
                Status = appointment.Status,
                DeclineReason=appointment.DeclineReason
            };

            string date = appointmentDTO.AppointmentDate?.ToString("MM/dd/yyyy");
            string time = appointmentDTO.AppointmentTime ?? null;
            try
            {
                bool sendMailResult = SendMail(appointmentDTO, string.Format(@"Hello,<br/> Appointment has been deleted. <br/> Date :- {0} <br/> Time Slot :- {1} <br/><br/> Thanks, <br/> PMS Team", date, time)).Result;
                if (sendMailResult)
                {
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(200);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        private List<AppointmentDTO> GetAppointmrentDTO(List<Appointment> appointments)
        {
            List<AppointmentDTO> appointmentDTOs = new List<AppointmentDTO>();

            foreach (var item in appointments)
            {
                appointmentDTOs.Add(new AppointmentDTO
                {
                    AppointmentId = item.Appointment_Id,
                    Title = item.Title,
                    AppointmentDate = item.Appointment_Date,
                    AppointmentTime = item.Appointment_Time,
                    Reason = item.Reason,
                    SchedularId = item.Schedular_Id,
                    PhysicianId = item.Physician_Id,
                    NurseId = item.Nurse_Id,
                    PatientId = item.Patient_Id,
                    CreatedOn = item.CreatedOn,
                    ModifiedOn = item.ModifiedOn,
                    IsActive = item.IsActive,
                    Status=item.Status,
                    DeclineReason=item.DeclineReason
                });
            }

            return appointmentDTOs;
        }

        private Appointment GetAppointment(AppointmentDTO appointmentDTO)
        {
            return new Appointment
            {
                Title = appointmentDTO.Title,
                Appointment_Date = appointmentDTO.AppointmentDate,
                Appointment_Time = appointmentDTO.AppointmentTime,
                Reason = appointmentDTO.Reason,
                Schedular_Id = appointmentDTO.SchedularId,
                Physician_Id = appointmentDTO.PhysicianId,
                Nurse_Id = appointmentDTO.NurseId,
                Patient_Id = appointmentDTO.PatientId,
                
                CreatedOn = appointmentDTO.CreatedOn,
                ModifiedOn = appointmentDTO.ModifiedOn,

                IsActive = appointmentDTO.IsActive,
                Status = appointmentDTO.Status,
                DeclineReason = appointmentDTO.DeclineReason,

            };
        }

        private async Task<bool> SendMail(AppointmentDTO appointmentDTO,  string body)
        {
            MailRequest request = new MailRequest();

            if (appointmentDTO.PatientId != null && appointmentDTO.PatientId != 0)
            {
                var registration = _unitOfWork.RegistrationRepo.GetById(appointmentDTO.PatientId?? 0);
                request.recipients.Add(registration.EmailId);
            }
            if (appointmentDTO.NurseId != null && appointmentDTO.NurseId != 0)
            {
                var registration = _unitOfWork.RegistrationRepo.GetById(appointmentDTO.NurseId ?? 0);
                request.recipients.Add(registration.EmailId);
            }
            if (appointmentDTO.PhysicianId != null && appointmentDTO.PhysicianId != 0)
            {
                var registration = _unitOfWork.RegistrationRepo.GetById(appointmentDTO.PhysicianId ?? 0);
                request.recipients.Add(registration.EmailId);
            }

            request.Subject = "PMS Appontment";
            request.Body = body;
            await _mailService.SendEmail(request);
            return true;
        }
    }
}
