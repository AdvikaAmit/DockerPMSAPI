using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.DTO
{
    public class UserRegistration
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? DOJ { get; set; }
        public string Status { get; set; }
        public string ContactNo { get; set; }
        public bool? IsActive { get; set; }
        public int? LoginAttempts { get; set; } 
        public bool? Is_SetDefault { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public string Role { get; set; }
        public int EmployeeId { get; set; }
    }
}
