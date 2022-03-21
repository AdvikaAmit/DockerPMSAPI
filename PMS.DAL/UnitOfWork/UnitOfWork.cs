using PMS.DAL.Repository;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            Allergies = new AllergyRepository(_context);
            Diagnosis = new DiagnosisRepository(_context);
            DrugDatas = new DrugDataRepository(_context);
            Logins = new LoginRepository(_context);
            RegistrationRepo = new RegistrationRepository(_context);
            Roledata = new RoleRepository(_context);
            UserRoles = new UserRolesRepository(_context);
            Appointment = new AppointmentRepository(_context);
            PatientDetails = new PatientDetailsRepository(_context);
            PatientAllergies = new PatientAllergyRepository(_context);
            AdminRepo = new AdminRepository(_context);
            Procedures = new ProcedureRepository(_context);
            PatientVisits = new PatientVisitDataRepository(_context);
            Notes = new NotesRepository(_context);
        }

        public IAllergyRepository Allergies { get; set; }
        public IDiagnosisRepository Diagnosis { get; set; }
        public IDrugDataRepository DrugDatas { get; set; }
        public IProcedureRepository Procedures { get; set; }
        public ILoginRepository Logins { get; set; }
        public IRegistrationRepository RegistrationRepo { get; set; }
        public IRoleRepository Roledata { get; set; }
        public IUserRolesRepository UserRoles { get; set; }
        public IAppointmentRepository Appointment { get; set; }
        public IPatientDetailsRepository PatientDetails { get; set; }
        public IPatientAllergyRepository PatientAllergies { get; set; }
        public IPatientVisitDataRepository PatientVisits { get; set; }
        public IAdminRepository AdminRepo { get; set; }
        public INotesRepository Notes { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
