using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repository
{
    public class AllergyRepository : GenericRepository<Allergy>, IAllergyRepository
    {
        public AllergyRepository(ApplicationDBContext context): base(context)
        {

        }
        public IEnumerable<Allergy> GetAllergies(int count)
        {
            return _context.Allergies.OrderByDescending(d => d.Allergy_Id).Take(count).ToList();
        }
    }
}
