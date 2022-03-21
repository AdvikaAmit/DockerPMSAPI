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
    public class MailControllerTest
    {
        Mock<IUnitOfWork> _unitOfWork { get; set; }
        Mock<IMailRepository> _mockmailRepository { get; set; }
        Mock<IRegistrationRepository> _mockRegistrationRepo { get; set; }
        IEnumerable<Registration> users { get; set; }
        MailController _mailController { get; set; }

        [SetUp]
        public void Setup()
        {
            users = GetListOfRegisteredUsers().AsEnumerable();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mockmailRepository = new Mock<IMailRepository>();
            _mockRegistrationRepo = new Mock<IRegistrationRepository>();
            _mailController = new MailController(_mockmailRepository.Object, _unitOfWork.Object);
        }

        [Test]
        public async Task SendForgotPwd_WhenCalled_ReturnOkResult()
        {
            //Arrange

            MailRequest request = new MailRequest { Subject = "Forgot Password", ToEmail = "vikram.rathod@citiustech.com" };
            var usedt = users.Where(d => d.UserId == 1).FirstOrDefault();

            _mockRegistrationRepo.Setup(x => x.GetAll()).Returns(users);
            _mockRegistrationRepo.Setup(x => x.GetUserByEmail("vikram.rathod@citiustech.com")).Returns(users.FirstOrDefault(x=>x.UserId == 1));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);

            //Act
            var actionResult = await _mailController.Send(request);
            var okResult = actionResult as OkObjectResult;

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task SendForgotPwd_WhenCalled_ReturnBadRequestResult()
        {
            //Arrange

            MailRequest request = new MailRequest { };
            var usedt = users.Where(d => d.UserId == 1).FirstOrDefault();
            _mockRegistrationRepo.Setup(x => x.GetUserByEmail(usedt.EmailId));
            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);

            //Act
            var actionResult = await _mailController.Send(request);
            var okresult = actionResult as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(okresult);
            Assert.AreEqual(400, okresult.StatusCode);

        }

        private List<Registration> GetListOfRegisteredUsers()
        {
            return new List<Registration>()
            {
            new Registration { UserId = 1, FirstName="VIkram", LastName="Rathod", EmployeeId=83, ContactNo="9960262290", EmailId="vikram.rathod@citiustech.com", DOB= Convert.ToDateTime("1990-06-21"),
                                   DOJ=Convert.ToDateTime("2022-03-16"), IsActive=true, Password="Admin@1234", Status="Active", Title="Mr." }
            };
        }

    }
}
