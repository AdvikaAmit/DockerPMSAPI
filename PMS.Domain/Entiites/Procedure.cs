using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PMS.Domain.Entiites
{
    public class Procedure
    {
        [Key]
        public int Procedure_ID { get; set; }

        public string Procedure_Code { get; set; }

        public string Procedure_Description { get; set; }

        public bool? Procedure_Is_Depricated { get; set; }

    }
}
