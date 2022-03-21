using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entiites
{
    public class PatientDetails
    {
        [Key]
        public int Patient_Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public DateTime? DOB { get; set; }
        public int Age { get; set; }
        public string ContactNo { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string Ethnicity { get; set; }
        public string Language { get; set; }
        public string Address { get; set; }
        public string Emergency_Title { get; set; }
        public string Emergency_FirstName { get; set; }
        public string Emergency_LastName { get; set; }
        public string Emergency_EmailId { get; set; }
        public string Emergency_ContactNo { get; set; }
        public string Emergency_Relation { get; set; }
        public string Emergency_Address { get; set; }
        public bool Access_To_Patient_Portal { get; set; }
        public bool Allergy_Details { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public bool Address_Same_As_Patient { get; set; }

    }
}
