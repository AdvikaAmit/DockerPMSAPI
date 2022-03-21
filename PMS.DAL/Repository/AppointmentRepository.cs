using PMS.Domain.DTO;
using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDBContext context) : base(context)
        {

        }

        public List<AppointmentListByUser> GetAppointmentListByNurseId(int UserId)
        {
            var dt = _context.Appointments.Where(d => d.Nurse_Id == UserId && d.IsActive == true).OrderByDescending(d => d.Appointment_Date).ToList();

            List<AppointmentListByUser> userlist = new List<AppointmentListByUser>();

            foreach (var item in dt)
            {
                var reguser = _context.Registration.Where(d => d.UserId == item.Physician_Id).FirstOrDefault();
                var reguserForPatientName = _context.Registration.Where(d => d.UserId == item.Patient_Id).FirstOrDefault();

                AppointmentListByUser user = new AppointmentListByUser();
                user.Title = item.Title;
                user.AppointmentId = item.Appointment_Id;
                user.AppointmentDate = item.Appointment_Date;
                user.AppointmentTime = item.Appointment_Time;
                user.Reason = item.Reason;
                user.PhysicianName = reguser.FirstName + " " + reguser.LastName;
                user.PatientName = reguserForPatientName.FirstName + " " + reguserForPatientName.LastName;
                userlist.Add(user);
            }

            return userlist;
        }

        public List<AppointmentListByUser> GetAppointmentListByPatientId(int UserId)
        {
            var dt = _context.Appointments.Where(d => d.Patient_Id == UserId && d.IsActive == true).OrderByDescending(d => d.Appointment_Date).ToList();

            List<AppointmentListByUser> userlist = new List<AppointmentListByUser>();

            foreach (var item in dt)
            {
                var reguser = _context.Registration.Where(d => d.UserId == item.Physician_Id).FirstOrDefault();

                AppointmentListByUser user = new AppointmentListByUser();
                user.Title = item.Title;
                user.AppointmentId = item.Appointment_Id;
                user.AppointmentDate = item.Appointment_Date;
                user.AppointmentTime = item.Appointment_Time;
                user.Reason = item.Reason;
                user.PhysicianName = reguser.FirstName + " " + reguser.LastName;
                userlist.Add(user);
            }

            return userlist;

        }

        public List<AppointmentListByUser> GetAppointmentListByPhysicianId(int UserId)
        {
            var dt = _context.Appointments.Where(d => d.Physician_Id == UserId && d.IsActive == true).OrderByDescending(d => d.Appointment_Date).ToList();

            List<AppointmentListByUser> userlist = new List<AppointmentListByUser>();

            foreach (var item in dt)
            {
                var reguser = _context.Registration.Where(d => d.UserId == item.Patient_Id).FirstOrDefault();

                AppointmentListByUser user = new AppointmentListByUser();
                user.Title = item.Title;
                user.AppointmentId = item.Appointment_Id;
                user.AppointmentDate = item.Appointment_Date;
                user.AppointmentTime = item.Appointment_Time;
                user.Reason = item.Reason;
                user.PatientName = reguser.FirstName + " " + reguser.LastName;
                user.Status = item.Status;
                userlist.Add(user);
            }

            return userlist;
        }
    }
}
