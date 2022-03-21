using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.DTO
{
    public class PatientVisitsDTO
    {       
        public int Visit_Id { get; set; }
        public DateTime? Visit_Date { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Blood_Pressure { get; set; }
        public string Body_Temperature { get; set; }
        public string Respiration_Rate { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }        
        public string Note { get; set; }

    }
}
