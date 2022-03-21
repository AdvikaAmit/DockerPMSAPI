using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entiites
{
    public class Appointment
    {
        [Key]
        public int Appointment_Id { get; set; }
        public string Title { get; set; }
        public DateTime? Appointment_Date { get; set; }
        public string Appointment_Time { get; set; }
        public string Reason { get; set; }
        public int? Schedular_Id { get; set; }
        public int? Physician_Id { get; set; }
        public int? Nurse_Id { get; set; }
        public int? Patient_Id { get; set; }       
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }

        public string Status { get; set; }
        public string DeclineReason { get; set; }


    }
}
