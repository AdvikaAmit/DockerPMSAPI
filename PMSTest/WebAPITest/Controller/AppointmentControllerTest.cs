using Moq;
using NUnit.Framework;
using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using PMS.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Domain.DTO;

namespace PMSTest.WebAPITest.Controller
{
    [TestFixture]
    public class AppointmentControllerTest
    {
        private Mock<IUnitOfWork> _unitOfWork { get; set; }
        private Mock<IMailRepository> _mockMailService { get; set; }
        public Mock<IAppointmentRepository> _mockAppointmenRepo { get; set; }
        public Mock<IRegistrationRepository> _mockRegistrationRepo { get; set; } //required for mail servce in Post and Put
        AppointmentController _appointmentController { get; set; }
        IEnumerable<Appointment> appointment;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mockMailService = new Mock<IMailRepository>();
            _mockAppointmenRepo = new Mock<IAppointmentRepository>();

            _mockRegistrationRepo = new Mock<IRegistrationRepository>();
            _appointmentController = new AppointmentController(_unitOfWork.Object, _mockMailService.Object);
        }

        [Test]
        public void Get_Returns_OkResult()
        {
            //Arrange
            appointment = GetListOfAppointment();
            _mockAppointmenRepo.Setup(x => x.GetAll()).Returns(appointment);
            _unitOfWork.Setup(x => x.Appointment).Returns(_mockAppointmenRepo.Object);

            // Act
            IActionResult actionResult = _appointmentController.Get();
            var result = actionResult as ObjectResult;

            var resultAppoint = result.Value as List<AppointmentDTO>;

            //Assert
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultAppoint);
            Assert.AreEqual(1, resultAppoint[0].AppointmentId);
        }

        [Test]
        public void Get_Returns_NotFoundResult()
        {
            //Arrange
            appointment = GetListOfAppointment();
            foreach (var item in appointment)
            {
                item.IsActive = false;
            }
            _mockAppointmenRepo.Setup(x => x.GetAll()).Returns(appointment);
            _unitOfWork.Setup(x => x.Appointment).Returns(_mockAppointmenRepo.Object);

            // Act
            IActionResult actionResult = _appointmentController.Get();
            var result = actionResult as ObjectResult;

            var resultAppoint = result.Value as List<AppointmentDTO>;

            //Assert
            Assert.AreEqual(result.StatusCode, 404);
            Assert.IsNotNull(result);
            Assert.IsNull(resultAppoint);
        }

        [Test]
        public void GetPhysicianAppointments_Returns_OkResult()
        {
            //Arrange
            var lstOfAppointmentListByUser = GetLisOfAppointmentListByUser();
            _mockAppointmenRepo.Setup(x => x.GetAppointmentListByPhysicianId(It.IsAny<int>())).Returns(lstOfAppointmentListByUser);
            _unitOfWork.Setup(x => x.Appointment).Returns(_mockAppointmenRepo.Object);

            // Act
            IActionResult actionResult = _appointmentController.GetPhysicianAppointments(It.IsAny<int>());
            var result = actionResult as ObjectResult;

            var resultAppoint = result.Value as List<AppointmentListByUser>;

            //Assert
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultAppoint);
            Assert.AreEqual(1, resultAppoint[0].AppointmentId);
        }

        [Test]
        public void GetPatientAppointments_Returns_OkResult()
        {
            //Arrange
            var lstOfAppointmentListByUser = GetLisOfAppointmentListByUser();
            _mockAppointmenRepo.Setup(x => x.GetAppointmentListByPatientId(It.IsAny<int>())).Returns(lstOfAppointmentListByUser);
            _unitOfWork.Setup(x => x.Appointment).Returns(_mockAppointmenRepo.Object);

            // Act
            IActionResult actionResult = _appointmentController.GetPatientAppointments(It.IsAny<int>());
            var result = actionResult as ObjectResult;

            var resultAppoint = result.Value as List<AppointmentListByUser>;

            //Assert
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultAppoint);
            Assert.AreEqual(1, resultAppoint[0].AppointmentId);
        }

        [Test]
        public void GetNurseAppointments_Returns_OkResult()
        {
            //Arrange
            var lstOfAppointmentListByUser = GetLisOfAppointmentListByUser();
            _mockAppointmenRepo.Setup(x => x.GetAppointmentListByNurseId(It.IsAny<int>())).Returns(lstOfAppointmentListByUser);
            _unitOfWork.Setup(x => x.Appointment).Returns(_mockAppointmenRepo.Object);

            // Act
            IActionResult actionResult = _appointmentController.GetNurseAppointments(It.IsAny<int>());
            var result = actionResult as ObjectResult;

            var resultAppoint = result.Value as List<AppointmentListByUser>;

            //Assert
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result);
            Assert.IsNotNull(resultAppoint);
            Assert.AreEqual(1, resultAppoint[0].AppointmentId);
        }

        [Test]
        public void Post_Returns_OkResult()
        {
            //Arrange
            var lstOfAppointmentListByUser = GetLisOfAppointmentListByUser();
            _mockAppointmenRepo.Setup(x => x.Add(It.IsAny<Appointment>()));
            _unitOfWork.Setup(x => x.Appointment).Returns(_mockAppointmenRepo.Object);

            _mockRegistrationRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(GetRegistrationEntity());

            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);

            _mockMailService.Setup(x => x.SendEmail(It.IsAny<MailRequest>()));

            // Act
            IActionResult actionResult = _appointmentController.Post(GetAppointmentDTO());
            var result = actionResult as StatusCodeResult;

            //Assert
            Assert.AreEqual(result.StatusCode, 201);

        }

        [Test]
        public void Put_Returns_OkResult()
        {
            //Arrange
            var lstOfAppointmentListByUser = GetLisOfAppointmentListByUser();
            _mockAppointmenRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(GetAppointmentEntity());
            _mockAppointmenRepo.Setup(x => x.Update(It.IsAny<Appointment>()));
            _unitOfWork.Setup(x => x.Appointment).Returns(_mockAppointmenRepo.Object);

            _mockRegistrationRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(GetRegistrationEntity());

            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);

            _mockMailService.Setup(x => x.SendEmail(It.IsAny<MailRequest>()));

            // Act
            IActionResult actionResult = _appointmentController.Put(1, GetAppointmentDTO());
            var result = actionResult as StatusCodeResult;

            //Assert
            Assert.AreEqual(result.StatusCode, 201);

        }

        [Test]
        public void Delete_Returns_OkResult()
        {
            //Arrange
            var lstOfAppointmentListByUser = GetLisOfAppointmentListByUser();
            _mockAppointmenRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(GetAppointmentEntity());
            _mockAppointmenRepo.Setup(x => x.Update(It.IsAny<Appointment>()));
            _unitOfWork.Setup(x => x.Appointment).Returns(_mockAppointmenRepo.Object);

            _mockRegistrationRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(GetRegistrationEntity());

            _unitOfWork.Setup(x => x.RegistrationRepo).Returns(_mockRegistrationRepo.Object);

            _mockMailService.Setup(x => x.SendEmail(It.IsAny<MailRequest>()));

            // Act
            IActionResult actionResult = _appointmentController.Delete(1);
            var result = actionResult as StatusCodeResult;

            //Assert
            Assert.AreEqual(result.StatusCode, 200);

        }

        private Appointment GetAppointmentEntity()
        {
            return new Appointment { Appointment_Id = 1, CreatedOn = new DateTime(), Patient_Id = 2, Physician_Id = 3, IsActive = true };
        }

        private Registration GetRegistrationEntity()
        {
            return new Registration()
            {
                UserId = 2,
                ContactNo = "95325752",
                EmailId = "myUTEmail90@gmail.com",
                DOB = new DateTime(1992, 6, 5),
                DOJ = new DateTime(2019, 8, 12),
                EmployeeId = 12,
                IsActive = true,
                LoginAttempts = 2,
                IsLoggedin = true,
                Is_SetDefault = true,
                CreatedOn = new DateTime(2019, 6, 5),
                CreatedBy = 8,
                UpdatedBy = 23,
                UpdatedOn = new DateTime(1992, 6, 10),
                FirstName = "mvsdv",
                Gender = "Male",
                Known_Languages = "English",
                LastName = "ghth",
                Password = "Pass#45678",
                Status = "Actve",
                Title = "dsfsd",
                UserCode = "jdf",
                Username = "MyUserUtName"

            };

        }

        private AppointmentDTO GetAppointmentDTO()
        {
            return new AppointmentDTO { AppointmentId = 1, CreatedOn = new DateTime(), PatientId = 2, PhysicianId = 3, IsActive = true };
        }

        private List<AppointmentListByUser> GetLisOfAppointmentListByUser()
        {
            return new List<AppointmentListByUser>()
            {
                new AppointmentListByUser {AppointmentId = 1, PatientId = 2, PhysicianId = 3 }
            };
        }

        private IEnumerable<Appointment> GetListOfAppointment()
        {
            return new List<Appointment>()
            {
                new Appointment {Appointment_Id = 1, CreatedOn =  new DateTime(),  Patient_Id = 2, Physician_Id = 3, IsActive = true }
            };
        }
    }
}
