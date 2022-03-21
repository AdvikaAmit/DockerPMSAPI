using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PMS.Domain.Entiites
{
    public class Diagnosis
    {
        [Key]
        public int Diagnosis_Id { get; set; }

        public string Diagnosis_Code { get; set; }

        public string Diagnosis_Description { get; set; }

        public bool? Diagnosis_Is_Depricated { get; set; }

    }
}
