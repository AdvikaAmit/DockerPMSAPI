using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entiites
{
    public class Notes
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? date { get; set; }
        public string Description { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Message { get; set; }
        public string UrgencyLevel { get; set; }
        public int ReplyId { get; set; }
        public bool IsActive { get; set; }
    }
}
