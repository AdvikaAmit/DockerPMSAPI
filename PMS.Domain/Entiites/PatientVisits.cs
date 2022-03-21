using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entiites
{
    public class PatientVisits
    {
        [Key]
        public int Visit_Id { get; set; }

        public DateTime? Visit_Date { get; set; }

        public string Height { get; set; }

        public string Weight { get; set; }

        public string Blood_Pressure { get; set; }

        public string Body_Temperature { get; set; }

        public string Respiration_Rate { get; set; }

        public int? UserId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public string Note { get; set; }


    }
}
