using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PMS.Domain.Entiites
{
    public class DrugData
    {
        [Key]
        public int Drug_ID { get; set; }

        public string Drug_Name { get; set; }

        public string Drug_Generic_Name { get; set; }

        public string Drug_Manufacture_Name { get; set; }

        public string Drug_Form { get; set; }

        public string Drug_Strength { get; set; }

    }
}
