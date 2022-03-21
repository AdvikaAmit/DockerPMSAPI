using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entiites
{
    public class Allergy
    {
        [Key]
        public int Allergy_Id { get; set; }
        public string Allergy_Code { get; set; }
        public string Allergy_Type { get; set; }
        public string Allergy_Name { get; set; }
        public string Allergy_Description { get; set; }
        public string Allergy_Clinical_Information { get; set; }
        public string Allergy_Source { get; set; }
        public string Allerginicity { get; set; }
    }
}
