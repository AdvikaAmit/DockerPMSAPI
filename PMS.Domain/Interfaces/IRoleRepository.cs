using PMS.Domain.Entiites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Interfaces
{
    public interface IRoleRepository : IGenericRepository<Roles>
    {
        Roles GetRoleByName(string Name);
        Roles GetRoleByUserId(int userid);
    }
}
