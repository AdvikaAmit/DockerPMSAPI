using PMS.Domain.Entiites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.DTO
{
    public class PatientVisitDetails
    {
        public PatientVisits patientVisits { get; set; }
        public List<PatientVisitDiagnosisDTO> diagnoses { get; set; }
        public List<PatientVisitProceduresDTO> procedures { get; set; }
        public List<PatientVisitMedicationDTO> medications { get; set; }
    }

    public class PatientVisitDiagnosisDTO
    {
        public int Id { get; set; }
        public int? Visit_Id { get; set; }
        public int? Diagnosis_Id { get; set; }
        public string Diagnosis_Code { get; set; }
        public string Diagnosis_Description { get; set; }
        public bool? Diagnosis_Is_Depricated { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
    }

    public class PatientVisitProceduresDTO
    {
        public int Id { get; set; }
        public int? Visit_Id { get; set; }
        public int? Procedure_Id { get; set; }
        public string? Procedure_Code { get; set; }
        public string? Procedure_Description { get; set; }
        public bool? Procedure__Is_Depricated { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
    }

    public class PatientVisitMedicationDTO
    {
        public int Id { get; set; }
        public int? Visit_Id { get; set; }
        public int? Drug_Id { get; set; }
        public string? Drug_Name { get; set; }
        public string? Drug_Generic_Name { get; set; }
        public string? Drug_Manufacture_Name { get; set; }
        public string? Drug_Form { get; set; }
        public string? Drug_Strength { get; set; }
        public string Dosage { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
    }
}
