using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using PMS.DAL.Repository;
using PMS.Domain.DTO;
using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using PMS.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSTest
{
    [TestFixture]
    public class LoginControllerTest
    {
        Mock<IUnitOfWork> _unitOfWork { get; set; }
        Mock<ILoginRepository> _mockLoginRepository { get; set; }
        Mock<IRegistrationRepository> _mockRegistrationRepo { get; set; }
        Mock<IMailRepository> _mockMailRepository { get; set; }
        Mock<IConfiguration> _mockConfiguration { get; set; }
        LoginController _loginController { get; set; }

        IEnumerable<Registration> users { get; set; }
        
        [SetUp]
        public void Setup()
        {
            users = GetListOfRegisteredUsers().AsEnumerable();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mockLoginRepository = new Mock<ILoginRepository>();
            _mockRegistrationRepo = new Mock<IRegistrationRepository>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockMailRepository = new Mock<IMailRepository>();
            _loginController = new LoginController(_unitOfWork.Object, _mockConfiguration.Object, _mockMailRepository.Object);
            
        }

        [Test]
        public void Login_WhenCalled_ReturnsOkResult()
        {
            Login login = new Login
            {
                email="amit.shinde@citiustech.com",
                password="Admin@1234"
            };

            _mockLoginRepository.Setup(x => x.Add(login));
            _mockLoginRepository.Setup(x => x.GetLoginData(login.email,login.password)).Returns(GetResponse());
            _unitOfWork.Setup(x => x.Logins).Returns(_mockLoginRepository.Object);

            IActionResult actionResult = (IActionResult)_loginController.GetLogin(login);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Login_WhenCalled_ReturnsBadRequestResult()
        {
            Login login = new Login { email = "", password = "" };
            LoginResponse response = new LoginResponse { };

            _mockLoginRepository.Setup(x => x.Add(login));
            _mockLoginRepository.Setup(x => x.GetLoginData(login.email, login.password)).Returns(response);
            _unitOfWork.Setup(x => x.Logins).Returns(_mockLoginRepository.Object);

            IActionResult actionResult = (IActionResult)_loginController.GetLogin(login);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        [Test]
        public async Task changepassword_WhenCalled_ReturnsOkResult()
        {
            ChangePassword pwd = new ChangePassword { email = "amit.shinde@citiustech.com", password = "Admin@1234", newpassword="Admin@123" };

            Registration user = new Registration
            {
                UserId = 1, 
                FirstName = "Amit",
                LastName = "Shinde",
                EmployeeId = 83,
                ContactNo = "9960262290",
                EmailId = "amit.shinde@citiustech.com",
                DOB = Convert.ToDateTime("1990-06-21"),
                DOJ = Convert.ToDateTime("2022-03-16"),
                IsActive = true,
                Password = "Admin@1234",
                Status = "Active",
                Title = "Mr.",               
            };

            _mockRegistrationRepo.Setup(x => x.Update(user));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);

            _mockRegistrationRepo.Setup(x => x.GetAll()).Returns(users);            
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);

            var actionResult = await _loginController.ChangePassword(pwd);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task changepassword_WhenCalled_ReturnsBadRequestResult()
        {
            ChangePassword pwd = new ChangePassword { };           
           
            _mockRegistrationRepo.Setup(x => x.GetAll()).Returns(users);
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);

            var actionResult = await _loginController.ChangePassword(pwd);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        [Test]
        public async Task Blockuser_WhenCalled_ReturnsOkResult()
        {
            Login pwd = new Login { email= "amit.shinde@citiustech.com", password="Admin@1234" };

            _mockRegistrationRepo.Setup(x => x.GetAll()).Returns(users);
            _mockRegistrationRepo.Setup(x => x.GetUserByEmail(pwd.email)).Returns(users.FirstOrDefault(d=>d.UserId == 1));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);

            var actionResult = await _loginController.Post(pwd);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task Blockuser_WhenCalled_ReturnsBadRequestResult()
        {
            Login pwd = new Login { };

            _mockRegistrationRepo.Setup(x => x.GetAll()).Returns(users);
            _mockRegistrationRepo.Setup(x => x.GetUserByEmail(pwd.email)).Returns(users.FirstOrDefault(d => d.UserId == 1));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);

            var actionResult = await _loginController.Post(pwd);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        [Test]
        public async Task Blockuser_WhenCalled_ReturnsNotFoundResult()
        {
            Login pwd = new Login { email="amit.shinde@citiustech.com", password="Admin@1234" };

            _mockRegistrationRepo.Setup(x => x.GetAll()).Returns(users);
            _mockRegistrationRepo.Setup(x => x.GetUserByEmail(pwd.email)).Returns(users.FirstOrDefault(d => d.UserId == 2));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);

            var actionResult = await _loginController.Post(pwd);
            var okResult = actionResult as NotFoundObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }

        private LoginResponse GetResponse()
        {
            LoginResponse response = new LoginResponse
            {
                UserId = 1,
                EmailId = "amit.shinde@citiustech.com",
                FirstName = "Amit",
                LastName = "Shinde",
                IsActive = true,
                Is_SetDefault = true,
                LoginAttempts = 0,
                Status = "Active"
            };

            return response;
        }

        private List<Registration> GetListOfRegisteredUsers()
        {
            return new List<Registration>()
            {
            new Registration { UserId = 1, FirstName="Amit", LastName="Shinde", EmployeeId=83, ContactNo="9960262290", EmailId="amit.shinde@citiustech.com", DOB= Convert.ToDateTime("1990-06-21"),
                                   DOJ=Convert.ToDateTime("2022-03-16"), IsActive=true, Password="Admin@1234", Status="Active", Title="Mr." }
            };
        }

    }
}
