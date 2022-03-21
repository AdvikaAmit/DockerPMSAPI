using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Domain.DTO;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailRepository _mailService;
        private readonly IUnitOfWork _unitOfWork;

        public MailController(IMailRepository mailService, IUnitOfWork unitOfWork)
        {
            _mailService = mailService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("SendForgotPwdMail")]
        public async Task<IActionResult> Send(MailRequest request)
        {
            try
            {
                var user = _unitOfWork.RegistrationRepo.GetUserByEmail(request.ToEmail);
                if (user != null)
                {
                    if(user.EmailId != null)
                    {
                        if (user.Status == "Inactive" || user.Status == "Block")
                        {
                            return BadRequest("PMS Account is " + user.Status);
                        }

                        request.Body = (@"Hello, Please find the below default password. <br/>
                                      Password : " + user.Password + " <br/>link to change the password. - <br/>" +
                                         "http://localhost:4200/changepwd/" + user.EmailId);

                        await _mailService.SendEmail(request);
                    }
                    
                }
                else
                {
                    return BadRequest("user not found");
                }

                return Ok(user.UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
