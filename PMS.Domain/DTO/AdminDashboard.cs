using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.DTO
{
    public class AdminDashboard
    {
        public int totalusers { get; set; }
        public int totalphysicians { get; set; }
        public int totalnurses { get; set; }
        public int totalpatients { get; set; }        
    }
}
