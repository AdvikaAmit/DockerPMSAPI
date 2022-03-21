using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
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
    public class RegistrationControllerTest
    {
        Mock<IUnitOfWork> _unitOfWork { get; set; }
        Mock<IMailRepository> _mailService { get; set; }
        Mock<IRegistrationRepository> _mockRegistrationRepo { get; set; }
        Mock<IRoleRepository> _mockRoleRepo { get; set; }
        Mock<IUserRolesRepository> _mockUserRolesRepo { get; set; }
        RegistrationController _registrationController { get; set; }
        IEnumerable<Registration> users { get; set; }
        IEnumerable<Roles> roles { get; set; }

        [SetUp]
        public void Setup()
        {
            users = GetListOfRegisteredUsers().AsEnumerable();
            roles = GetListOfRoles().AsEnumerable();
            _mockRegistrationRepo = new Mock<IRegistrationRepository>();
            _mockRoleRepo = new Mock<IRoleRepository>();
            _mockUserRolesRepo = new Mock<IUserRolesRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mailService = new Mock<IMailRepository>();
            _registrationController = new RegistrationController(_unitOfWork.Object, _mailService.Object);
        }

        [Test]
        public void GetAllUsers_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockRegistrationRepo.Setup(x => x.GetAll()).Returns(users);
            _mockRoleRepo.Setup(x => x.GetRoleByUserId(1)).Returns(roles.FirstOrDefault(d=>d.Id == 1));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);
            _unitOfWork.Setup(x => x.Roledata).Returns(_mockRoleRepo.Object);


            // Act
            IActionResult actionResult = (IActionResult)_registrationController.GetAll();
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetAllHospitalUsers_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockRegistrationRepo.Setup(x => x.GetAll()).Returns(users);
            _mockRoleRepo.Setup(x => x.GetRoleByUserId(1)).Returns(roles.FirstOrDefault(d => d.Id == 1));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);
            _unitOfWork.Setup(x => x.Roledata).Returns(_mockRoleRepo.Object);


            // Act
            IActionResult actionResult = (IActionResult)_registrationController.Get();
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetAllPatientUsers_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockRegistrationRepo.Setup(x => x.GetAll()).Returns(users);
            _mockRoleRepo.Setup(x => x.GetRoleByUserId(1)).Returns(roles.FirstOrDefault(d => d.Id == 1));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);
            _unitOfWork.Setup(x => x.Roledata).Returns(_mockRoleRepo.Object);


            // Act
            IActionResult actionResult = (IActionResult)_registrationController.GetPatientUsers();
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetUsersById_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockRegistrationRepo.Setup(x => x.GetById(1)).Returns(users.FirstOrDefault(d=>d.UserId == 1));
            _mockRoleRepo.Setup(x => x.GetRoleByUserId(1)).Returns(roles.FirstOrDefault(d => d.Id == 1));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);
            _unitOfWork.Setup(x => x.Roledata).Returns(_mockRoleRepo.Object);


            // Act
            IActionResult actionResult = (IActionResult)_registrationController.Get(1);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetUsersById_WhenCalled_ReturnsNotFoundResult()
        {
            //Arrange
            _mockRegistrationRepo.Setup(x => x.GetById(2)).Returns(users.FirstOrDefault(d => d.UserId == 2));
            _mockRoleRepo.Setup(x => x.GetRoleByUserId(1)).Returns(roles.FirstOrDefault(d => d.Id == 1));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);
            _unitOfWork.Setup(x => x.Roledata).Returns(_mockRoleRepo.Object);


            // Act
            IActionResult actionResult = (IActionResult)_registrationController.Get(2);
            var okResult = actionResult as NotFoundObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }

        [Test]
        public async Task Post_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            UserRegistration user = new UserRegistration
            {
                //UserId = 1, 
                FirstName="Amit", 
                LastName="Shinde", 
                EmployeeId=83, 
                ContactNo="9960262290", 
                EmailId="amit.shinde@citiustech.com", 
                DOB= Convert.ToDateTime("1990-06-21"),
                DOJ=Convert.ToDateTime("2022-03-16"), 
                IsActive=true, Password="Admin@1234", 
                Status="Active", 
                Title="Mr.",
                Role="Admin"
            };

            UserRoles userRoles = new UserRoles
            {
                 Id=1,
                 RoleId=1,
                 UserId=1
            };


            _mockRegistrationRepo.Setup(x => x.Add(users.FirstOrDefault(x=>x.UserId == 1)));
            _mockUserRolesRepo.Setup(x => x.Add(userRoles));
            _mockRegistrationRepo.Setup(x => x.GetById(1)).Returns(users.FirstOrDefault(d => d.UserId == 1));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);
            _mockRoleRepo.Setup(x => x.GetRoleByName("Admin")).Returns(roles.FirstOrDefault(d => d.Id == 1));
            //_mockRoleRepo.Setup(x => x.GetRoleByUserId(1)).Returns(roles.FirstOrDefault(d => d.Id == 1));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);
            _unitOfWork.Setup(x => x.Roledata).Returns(_mockRoleRepo.Object);
            _unitOfWork.Setup(x => x.UserRoles).Returns(_mockUserRolesRepo.Object);

            // Act
            var actionResult = await _registrationController.Post(user);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);           
        }

        [Test]
        public async Task Post_WhenCalled_ReturnsBadRequestResult()
        {
            //Arrange

            UserRegistration user = new UserRegistration{ };

            _mockRegistrationRepo.Setup(x => x.Add(users.FirstOrDefault(x => x.UserId == 1)));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);

            // Act
            var actionResult = await _registrationController.Post(user);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        private List<Registration> GetListOfRegisteredUsers()
        {
            return new List<Registration>()
            {
            new Registration { UserId = 1, FirstName="Amit", LastName="Shinde", EmployeeId=83, ContactNo="9960262290", EmailId="amit.shinde@citiustech.com", DOB= Convert.ToDateTime("1990-06-21"),
                                   DOJ=Convert.ToDateTime("2022-03-16"), IsActive=true, Password="Admin@1234", Status="Active", Title="Mr." }
            };
        }

        private List<Roles> GetListOfRoles()
        {
            return new List<Roles>()
            {
               new Roles { Id = 1, Name="Admin" },
               new Roles { Id = 2, Name="Patient" },
               new Roles { Id = 3, Name="Nurse" },
               new Roles { Id = 4, Name="Physician" }
            };
        }       

    }
}
