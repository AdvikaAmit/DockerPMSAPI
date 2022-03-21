using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAllergyRepository Allergies { get; set; }
        IDiagnosisRepository Diagnosis { get; set; }
        IDrugDataRepository DrugDatas { get; set; }
        IProcedureRepository Procedures { get; set; }
        ILoginRepository Logins { get; set; }
        IRegistrationRepository RegistrationRepo { get; set; }
        IRoleRepository Roledata { get; set; }
        IUserRolesRepository UserRoles { get; set; }
        IAppointmentRepository Appointment { get; set; }
        IPatientDetailsRepository PatientDetails { get; set; }
        IPatientAllergyRepository PatientAllergies { get; set; }
        IPatientVisitDataRepository PatientVisits { get; set; }
        INotesRepository Notes { get; set; }
        int Complete();
    }
}
