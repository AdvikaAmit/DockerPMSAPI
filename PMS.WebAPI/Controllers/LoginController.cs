using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PMS.Domain.DTO;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IConfiguration _configuration;
        private readonly IMailRepository _mailService;

        public LoginController(IUnitOfWork unitOfWork, IConfiguration configuration, IMailRepository mailService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mailService = mailService;
        }

        [HttpPost]
        public IActionResult GetLogin(Login logindata)
        {
            var user = _unitOfWork.Logins.GetLoginData(logindata.email, logindata.password);

            if (!string.IsNullOrWhiteSpace(user.EmailId))
            {  
                if(user.Status == "Inactive" || user.Status == "Block")
                {
                    return BadRequest("PMS Account is "+ user.Status);
                }

                var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("id", user.UserId.ToString()),
                        new Claim("FirstName", user.FirstName),
                        new Claim("LastName", user.LastName),
                        new Claim("UserName", user.EmailId)
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["jwt:Issuer"],
                                                 _configuration["jwt:Audience"],
                                                 claims,
                                                 expires: DateTime.UtcNow.AddDays(1),
                                                 signingCredentials: signIn);

                var gen_token = new JwtSecurityTokenHandler().WriteToken(token);

                user.access_token = gen_token;

            }
            else
            {
                return BadRequest("Invalid login data");
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePassword change)
        {
            var user = _unitOfWork.RegistrationRepo.GetAll();
            var userdt = user.Where(d => d.EmailId == change.email && d.Password == change.password).FirstOrDefault();

            if (userdt != null)
            {
                userdt.Password = change.newpassword;
                userdt.Is_SetDefault = false;
                _unitOfWork.RegistrationRepo.Update(userdt);
                _unitOfWork.Complete();

                //send mail for password update

                MailRequest request = new MailRequest();

                request.ToEmail = userdt.EmailId;
                request.Subject = "PMS Account Password Update";
                request.Body = (@"Hello, Your PMS Account Password Updated. <br/> New Password -"+ userdt.Password);

                await _mailService.SendEmail(request);
                
            }
            else
            {
                return BadRequest("Password is invalid");
            }

            return Ok(userdt.UserId);
        }

        [HttpPost]
        [Route("BlockUser")]
        public async Task<IActionResult> Post([FromBody] Login loginuser)
        {
            if (string.IsNullOrWhiteSpace(loginuser.email))
            {
                return BadRequest("Bad Request");
            }

            var user = _unitOfWork.RegistrationRepo.GetUserByEmail(loginuser.email);

            if (user == null)
            {
                return NotFound("Not Found");
            }

            user.IsActive = false;
            user.UpdatedOn = DateTime.Now;
            _unitOfWork.RegistrationRepo.Update(user);
            _unitOfWork.Complete();

            //send mail for blocking

            MailRequest request = new MailRequest();

            request.ToEmail = user.EmailId;
            request.Subject = "Block PMS Account";
            request.Body = (@"Hello, Your PMS Account blocked by 3 unsuccessful login attempts. <br/> contact administrator for this..");

            await _mailService.SendEmail(request);

            return Ok(user.UserId);

        }


    }
}
