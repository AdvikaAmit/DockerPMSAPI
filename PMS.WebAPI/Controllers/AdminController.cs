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
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailRepository _mailService;

        public AdminController(IUnitOfWork unitOfWork, IMailRepository mailService)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Get()
        {
            var registration = _unitOfWork.RegistrationRepo.GetAll().Where(d => d.IsActive == true);

            AdminDashboard admin = new AdminDashboard();
            admin.totalphysicians = admin.totalnurses = admin.totalpatients = admin.totalusers = 0;

            foreach (var item in registration)
            {
                var dt = _unitOfWork.Roledata.GetRoleByUserId(item.UserId);

                if (dt.Name == "Physician")
                {
                    admin.totalphysicians++;
                }
                if (dt.Name == "Nurse")
                {
                    admin.totalnurses++;
                }
                if (dt.Name == "Patient")
                {
                    admin.totalpatients++;
                }
                
                admin.totalusers++;                
            }

            return Ok(admin);
        }

        [HttpPost]
        [Route("UpdateUserStatus")]
        public async Task<IActionResult> UpdateUserStatus(UserAction user)
        {
            if(user == null)
            {
                return BadRequest("BadRequest");
            }

            var userdt = _unitOfWork.RegistrationRepo.GetById(user.UserId);
            userdt.Status = user.Status;
            _unitOfWork.RegistrationRepo.Update(userdt);
            _unitOfWork.Complete();

            //send mail 

            if(user.Status == "Block")
            {
                MailRequest request = new MailRequest();

                request.ToEmail = userdt.EmailId;
                request.Subject = "PMS Account Blocked";
                request.Body = (@"Hello " + userdt.FirstName + "<br/> Your Account has been Blocked <br/> If you have any questions about your account, plesase Contact to Administrator");

                await _mailService.SendEmail(request);
            }            

            return Ok(userdt.UserId);

        }
    }
}
