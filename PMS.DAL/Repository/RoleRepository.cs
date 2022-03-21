using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repository
{
    public class RoleRepository : GenericRepository<Roles>, IRoleRepository
    {
        public RoleRepository(ApplicationDBContext context): base(context)
        {

        }

        public Roles GetRoleByName(string Name)
        {
            return _context.Roles.Where(d => d.Name.Contains(Name)).FirstOrDefault();
        }

        public Roles GetRoleByUserId(int userid)
        {
            var userRoles = _context.UserRoles.Where(d => d.UserId == userid).FirstOrDefault();
            var role = _context.Roles.Where(d => d.Id == userRoles.RoleId).FirstOrDefault();
            return role;
        }
    }
}
