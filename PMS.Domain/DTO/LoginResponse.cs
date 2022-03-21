using PMS.Domain.Entiites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.DTO
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public int? EmployeeId { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }  
        public bool? IsActive { get; set; }
        public int? LoginAttempts { get; set; }  
        public bool? Is_SetDefault { get; set; }
        public List<UserRoles> roles { get; set; }
        public string Status { get; set; }
        public string access_token { get; set; }
    }

    
}
