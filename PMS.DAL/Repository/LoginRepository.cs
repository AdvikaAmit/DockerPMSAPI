using Microsoft.EntityFrameworkCore;
using PMS.Domain.DTO;
using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repository
{
    public class LoginRepository : GenericRepository<Login>, ILoginRepository
    {
        public LoginRepository(ApplicationDBContext context) : base(context)
        {

        }

        public LoginResponse GetLoginData(string email, string password)
        {
            LoginResponse response = new LoginResponse();
            var regdata =  _context.Registration.Where(d => d.EmailId == email && d.Password == password & d.IsActive == true).FirstOrDefault();
            if(regdata != null)
            {
                var userroles = _context.UserRoles.Where(d => d.UserId == regdata.UserId).ToList();
                response = new LoginResponse
                {
                    UserId = regdata.UserId,
                    EmailId = regdata.EmailId,
                    FirstName = regdata.FirstName,
                    LastName = regdata.LastName,
                    IsActive = regdata.IsActive,
                    Is_SetDefault = regdata.Is_SetDefault,
                    LoginAttempts = regdata.LoginAttempts,
                    Status = regdata.Status
                };

                response.roles = new List<UserRoles>();
                foreach (var item in userroles)
                {
                    UserRoles role = new UserRoles();
                    role.UserId = item.UserId;
                    role.RoleId = item.RoleId;
                    response.roles.Add(role);
                }
            }            

            return response;
        }        
    }
}
