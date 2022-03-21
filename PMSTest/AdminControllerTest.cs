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
    public class AdminControllerTest
    {
        Mock<IUnitOfWork> _unitOfWork { get; set; }
        Mock<IMailRepository> _mailService { get; set; }
        Mock<IRegistrationRepository> _mockRegistrationRepo { get; set; }
        Mock<IRoleRepository> _mockRoleRepo { get; set; }
        IEnumerable<Registration> users { get; set; }
        IEnumerable<Roles> roles { get; set; }
        AdminController _adminController { get; set; }

        [SetUp]
        public void Setup()
        {
            users = GetListOfRegisteredUsers().AsEnumerable();
            roles = GetListOfRoles().AsEnumerable();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mockRegistrationRepo = new Mock<IRegistrationRepository>();
            _mockRoleRepo = new Mock<IRoleRepository>();
            _mailService = new Mock<IMailRepository>();
            _adminController = new AdminController(_unitOfWork.Object, _mailService.Object);
        }

        [Test]
        public void GetDashboardData_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockRegistrationRepo.Setup(x => x.GetAll()).Returns(users);
            _mockRoleRepo.Setup(x => x.GetRoleByUserId(1)).Returns(roles.FirstOrDefault(d => d.Id == 1));
            _mockRoleRepo.Setup(x => x.GetRoleByUserId(2)).Returns(roles.FirstOrDefault(d => d.Id == 2));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);
            _unitOfWork.Setup(x => x.Roledata).Returns(_mockRoleRepo.Object);


            // Act
            IActionResult actionResult = (IActionResult)_adminController.Get();
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task UpdateUserStatus_WhenCalled_ReturnsOkResult()
        {
            //Arrange
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
                Title = "Mr."
            };

            UserAction action = new UserAction { UserId = 1, Status = "Active" };


            _mockRegistrationRepo.Setup(x => x.Update(user));
            _mockRegistrationRepo.Setup(x => x.GetById(1)).Returns(users.FirstOrDefault(x=>x.UserId == 1));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);          


            // Act
            var actionResult = await _adminController.UpdateUserStatus(action);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task UpdateUserStatus_WhenCalled_ReturnsBadRequestResult()
        {
            //Arrange            

            UserAction action = null;
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);


            // Act
            var actionResult = await _adminController.UpdateUserStatus(action);
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
                                   DOJ=Convert.ToDateTime("2022-03-16"), IsActive=true, Password="Admin@1234", Status="Active", Title="Mr." },

                new Registration { UserId = 2, FirstName="Vikram", LastName="Rathod", EmployeeId=53, ContactNo="8860262290", EmailId="vikram.rathod@citiustech.com", DOB= Convert.ToDateTime("1990-06-21"),
                                   DOJ=Convert.ToDateTime("2022-03-16"), IsActive=true, Password="Vikram@1234", Status="Active", Title="Mr." },
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
