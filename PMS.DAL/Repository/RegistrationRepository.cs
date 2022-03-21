using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repository
{
    public class RegistrationRepository : GenericRepository<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(ApplicationDBContext context) : base(context)
        {

        }

        public Registration GetRegisteredUser(string email, string password)
        {
           return _context.Registration.Where(d => d.EmailId == email && d.Password == password).FirstOrDefault();
        }

        public Registration GetUserByEmail(string email)
        {
            return _context.Registration.Where(d => d.EmailId == email && d.IsActive == true).FirstOrDefault();
        }        
    }
}
