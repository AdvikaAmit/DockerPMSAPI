using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repository
{
   public class PatientAllergyRepository : GenericRepository<PatientAllergies>, IPatientAllergyRepository
    {
        public PatientAllergyRepository(ApplicationDBContext context) : base(context)
        {

        }
        public IEnumerable<PatientAllergies> GetAllergies(int count)
        {
            return _context.PatientAllergies.OrderByDescending(d => d.Allergy_Id).Take(count).ToList();
        }

        public List<PatientAllergies> GetPatientAllergyDetails(int id)
        {
            List<PatientAllergies> patientAllergies = new List<PatientAllergies>();
            var allergyData = _context.PatientAllergies.Where(d => d.UserId == id).ToList();
            if (allergyData == null)
            {
                patientAllergies = null;
            }
            else
            {
                patientAllergies = allergyData;
            }

            return allergyData;
        }

       
    }
}
