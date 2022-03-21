using PMS.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Interfaces
{
    public interface IMailRepository
    {
        Task SendEmail(MailRequest mailRequest);
    }
}
