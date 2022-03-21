using Microsoft.AspNetCore.Mvc;
using PMS.Domain.DTO;
using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailRepository _mailService;

        public RegistrationController(IUnitOfWork unitOfWork, IMailRepository mailService)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {

            var registration = _unitOfWork.RegistrationRepo.GetAll().Where(d => d.IsActive == true);

            List<UserRegistration> userlist = new List<UserRegistration>();

            foreach (var item in registration)
            {
                var dt = _unitOfWork.Roledata.GetRoleByUserId(item.UserId);

                UserRegistration user = new UserRegistration
                {
                    Title = item.Title,
                    Name = item.FirstName + " " + item.LastName,
                    EmailId = item.EmailId,
                    EmployeeId = item.EmployeeId == null ? 0 : (int)item.EmployeeId,
                    UserId = item.UserId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    DOB = item.DOB,
                    DOJ = item.DOJ,
                    Status = item.Status,
                    Role = dt.Name == null ? "" : dt.Name
                };
                userlist.Add(user);
            }

            return Ok(userlist);
        }

        // GET: api/<RegistrationController>
        [HttpGet]       
        public IActionResult Get()
        {           

            var registration = _unitOfWork.RegistrationRepo.GetAll().Where(d=>d.IsActive == true).OrderByDescending(d=>d.DOJ);

            List<UserRegistration> userlist = new List<UserRegistration>();

            foreach (var item in registration)
            {
                var dt = _unitOfWork.Roledata.GetRoleByUserId(item.UserId);

                if(dt.Name != "Patient")
                {
                    UserRegistration user = new UserRegistration
                    {
                        Title = item.Title,
                        Name = item.FirstName + " " + item.LastName,
                        EmailId = item.EmailId,
                        ContactNo = item.ContactNo,
                        EmployeeId = item.EmployeeId == null ?  0 : (int)item.EmployeeId,
                        UserId = item.UserId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        DOB = item.DOB,
                        DOJ = item.DOJ,
                        Status = item.Status,
                        Role = dt.Name == null ? "": dt.Name
                    };
                    userlist.Add(user);
                }              
            }

            return Ok(userlist);
        }

        [HttpGet]
        [Route("GetPatientList")]
        public IActionResult GetPatientUsers()
        {

            var registration = _unitOfWork.RegistrationRepo.GetAll().Where(d => d.IsActive == true).OrderByDescending(d=>d.DOJ);

            List<UserRegistration> userlist = new List<UserRegistration>();

            foreach (var item in registration)
            {
                var dt = _unitOfWork.Roledata.GetRoleByUserId(item.UserId);

                if (dt.Name == "Patient")
                {
                    UserRegistration user = new UserRegistration
                    {
                        Title = item.Title,
                        Name = item.FirstName + " " + item.LastName,
                        EmailId = item.EmailId,
                        EmployeeId = item.EmployeeId == null ? 0 : (int)item.EmployeeId,
                        UserId = item.UserId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        DOB = item.DOB,                        
                        Status = item.Status,
                        Role = dt.Name == null ? "" : dt.Name,
                        CreatedOn = item.CreatedOn
                    };
                    userlist.Add(user);
                }
            }

            return Ok(userlist);
        }

        // GET api/<RegistrationController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var registration = _unitOfWork.RegistrationRepo.GetById(id);

            UserRegistration user = new UserRegistration();

            if(registration == null)
            {
                return NotFound("Not Found");
            }

            var dt = _unitOfWork.Roledata.GetRoleByUserId(registration.UserId);

            user = new UserRegistration
            {
                Title = registration.Title,
                Name = registration.FirstName + " " + registration.LastName,
                EmailId = registration.EmailId,
                ContactNo = registration.ContactNo,
                EmployeeId = registration.EmployeeId == null ? 0 : (int)registration.EmployeeId,
                UserId = registration.UserId,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                DOB = registration.DOB,
                Role = dt.Name == null ? "" : dt.Name
            };
           
            return Ok(user);
        }

        // POST api/<RegistrationController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRegistration user)
        {
            if (user.FirstName == null)
            {
                return BadRequest("Bad Request");
            }

            if(user.UserId !=0)
            {
                var userdt = _unitOfWork.RegistrationRepo.GetById(user.UserId);

                userdt.Title = user.Title;
                userdt.FirstName = user.FirstName;
                userdt.LastName = user.LastName;
                userdt.EmailId = user.EmailId;
                userdt.ContactNo = user.ContactNo;
                userdt.DOB = user.DOB;
                userdt.Username = user.EmailId;
                userdt.EmployeeId = user.EmployeeId;
                userdt.UpdatedOn = DateTime.Now;
                _unitOfWork.RegistrationRepo.Update(userdt);
                _unitOfWork.Complete();

                //Roles roledt = new Roles();
                //roledt = _unitOfWork.Roledata.GetRoleByName(user.Role);
                //_unitOfWork.UserRoles.Add(new UserRoles { UserId = registration.UserId, RoleId = roledt.Id });
                //_unitOfWork.Complete();
                return Ok(userdt.UserId);
            }
            else
            {
                Registration registration = new Registration();
                registration.Title = user.Title;
                registration.FirstName = user.FirstName;
                registration.LastName = user.LastName;
                registration.EmailId = user.EmailId;
                registration.ContactNo = user.ContactNo;
                registration.DOB = user.DOB;
                registration.Username = user.EmailId;
                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    registration.Password = user.Password;
                    registration.Is_SetDefault = false;
                }
                else
                {
                    registration.Password = "Password123@";
                    registration.Is_SetDefault = true;
                }

                registration.EmployeeId = user.EmployeeId;
                registration.CreatedOn = DateTime.Now;
                registration.IsActive = user.IsActive;
                registration.Status = user.Status;
                registration.DOJ = DateTime.Now;
                registration.LoginAttempts = user.LoginAttempts;
                _unitOfWork.RegistrationRepo.Add(registration);
                _unitOfWork.Complete();

                Roles roledt = new Roles();
                roledt = _unitOfWork.Roledata.GetRoleByName(user.Role);
                _unitOfWork.UserRoles.Add(new UserRoles { UserId = registration.UserId, RoleId = roledt.Id });
                _unitOfWork.Complete();

               
                MailRequest request = new MailRequest();

                request.ToEmail = registration.EmailId;
                request.Subject = "PMS Registration";
                request.Body = (@"Hello, Thank you for registration <br/> Find the below default password. <br/>
                                  Password : " + registration.Password);

                await _mailService.SendEmail(request);

                return Ok(registration.UserId);
            }           
        }
       
        // PUT api/<RegistrationController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserRegistration user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            var userdt = _unitOfWork.RegistrationRepo.GetById(id);
            
            userdt.Title = user.Title;
            userdt.FirstName = user.FirstName;
            userdt.LastName = user.LastName;
            userdt.EmailId = user.EmailId;
            userdt.ContactNo = user.ContactNo;
            userdt.DOB = user.DOB;
            userdt.Username = user.EmailId;
            userdt.EmployeeId = user.EmployeeId;
            userdt.UpdatedOn = DateTime.Now;
            _unitOfWork.RegistrationRepo.Update(userdt);
            _unitOfWork.Complete();

            //Roles roledt = new Roles();
            //roledt = _unitOfWork.Roledata.GetRoleByName(user.Role);
            //_unitOfWork.UserRoles.Add(new UserRoles { UserId = registration.UserId, RoleId = roledt.Id });
            //_unitOfWork.Complete();
            return Ok(userdt.UserId);
        }

        // DELETE api/<RegistrationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
