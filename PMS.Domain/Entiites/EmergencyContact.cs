using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entiites
{
    public class EmergencyContact
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string CountryCode { get; set; }

        public string ContactNo { get; set; }

        public string Address { get; set; }

        public string RelationShip { get; set; }

        public bool? Is_Allowed { get; set; }

        public int? UserId { get; set; }

    }
}
