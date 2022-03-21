using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entiites
{
    public class PatientVisitProcedures
    {
        public int Id { get; set; }

        public int? Visit_Id { get; set; }

        public int? Procedure_Id { get; set; }

        public string Description { get; set; }
        public string Note { get; set; }
    }
}
