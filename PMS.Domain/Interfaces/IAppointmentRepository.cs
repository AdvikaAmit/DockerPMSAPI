using PMS.Domain.DTO;
using PMS.Domain.Entiites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        List<AppointmentListByUser> GetAppointmentListByPhysicianId(int UserId);
        List<AppointmentListByUser> GetAppointmentListByPatientId(int UserId);
        List<AppointmentListByUser> GetAppointmentListByNurseId(int UserId);
    }
}
