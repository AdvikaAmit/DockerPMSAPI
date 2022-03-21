using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entiites
{
    public class Log
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
    }
}
