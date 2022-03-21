using Microsoft.EntityFrameworkCore;
using PMS.Domain.Entiites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<DrugData> DrugDatas { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<EmergencyContact> EmergencyContact { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Registration> Registration { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<PatientVisits> PatientVisits { get; set; }
        public DbSet<PatientAllergies> PatientAllergies { get; set; }
        public DbSet<PatientVisitDiagnosis> PatientVisitDiagnoses { get; set; }
        public DbSet<PatientVisitProcedures> PatientVisitProcedures { get; set; }
        public DbSet<PatientVisitMedication> PatientVisitMedications { get; set; }        
        public DbSet<PatientDetails> PatientDetails { get; set; }

    }
}
