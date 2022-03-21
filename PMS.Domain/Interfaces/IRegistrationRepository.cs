using PMS.Domain.DTO;
using PMS.Domain.Entiites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Interfaces
{
    public interface IRegistrationRepository : IGenericRepository<Registration>
    {
        Registration GetRegisteredUser(string email,string password);
        Registration GetUserByEmail(string email);        

    }
}
