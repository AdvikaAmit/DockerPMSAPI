using PMS.Domain.DTO;
using PMS.Domain.Entiites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Interfaces
{
    public interface IPatientVisitDataRepository : IGenericRepository<PatientVisitDetails>
    {
        List<PatientVisits> GetPatientVisitHistoryList(int patientId);
        List<PatientVisitsDTO> GetPatientVisitHistoryListForPhysician();
        PatientVisitDetails GetPatientVisitDetails(int visitId);
        int AddPatientVisit(PatientVisits visit);
        int AddVisitDiagnosis(List<PatientVisitDiagnosis> diagnosis);
        int AddVisitProcedures(List<PatientVisitProcedures> procedures);
        int AddVisitMedications(List<PatientVisitMedication> medication);
    }
}
