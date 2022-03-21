using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.DTO
{
    public class AppointmentDTO
    {
        public int AppointmentId { get; set; }
        public string Title { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Reason { get; set; }
        public int? SchedularId { get; set; }
        public int? PhysicianId { get; set; }
        public int? NurseId { get; set; }
        public int? PatientId { get; set; }       
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public string PhysicianName { get; set; }
        public string PatientName { get; set; }
        public string Status { get; set; }      
        public string DeclineReason { get; set; }


    }
}
