using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
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
   public class PatientDetailsControllerTest
    {
        private Mock<IUnitOfWork> _unitOfWork { get; set; }
        public Mock<IPatientDetailsRepository> _mockPatientDetailsRepo { get; set; }
        PatientDetailsController _patientDetailsController { get; set; }
        IEnumerable<PatientDetails> patientDetails;
        IEnumerable<Registration> patientDemographics;

        [SetUp]
        public void Setup()
        {
            patientDetails = GetListOfPatientDetails().AsEnumerable();
            patientDemographics = GetListOfPatientDemographicDetails().AsEnumerable();
            _mockPatientDetailsRepo = new Mock<IPatientDetailsRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _patientDetailsController = new PatientDetailsController(_unitOfWork.Object);
        }

        [Test]
        public void GetPatientDetails_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockPatientDetailsRepo.Setup(x => x.GetAll()).Returns(patientDetails);
            _unitOfWork.Setup(x => x.PatientDetails).Returns(_mockPatientDetailsRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientDetailsController.GetPatientDetailList();
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void GetPatientDetailsById_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockPatientDetailsRepo.Setup(x => x.GetById(1)).Returns(patientDetails.FirstOrDefault(x => x.Patient_Id == 1));
            _unitOfWork.Setup(x => x.PatientDetails).Returns(_mockPatientDetailsRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientDetailsController.GetPatientDetailById(1);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void GetPatientDetailsById_WhenCalled_ReturnsNotFoundBadRequest()
        {
            //Arrange
            _mockPatientDetailsRepo.Setup(x => x.GetById(2)).Returns(patientDetails.FirstOrDefault(x => x.Patient_Id == 2));
            _unitOfWork.Setup(x => x.PatientDetails).Returns(_mockPatientDetailsRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientDetailsController.GetPatientDetailById(2);
            var okResult = actionResult as NotFoundObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }
        [Test]
        public void GetPatientDemographicDetails_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockPatientDetailsRepo.Setup(x => x.GetPatientDemographicDetails(26)).Returns(patientDetails.FirstOrDefault(x => x.UserId == 26));
            _unitOfWork.Setup(x => x.PatientDetails).Returns(_mockPatientDetailsRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientDetailsController.GetPatientDemoGraphicDetails(26);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetPatientDemographicDetails_WhenCalled_ReturnsNotFoundBadRequest()
        {
            //Arrange
            _mockPatientDetailsRepo.Setup(x => x.GetPatientDemographicDetails(27)).Returns(patientDetails.FirstOrDefault(x => x.UserId == 27));
            _unitOfWork.Setup(x => x.PatientDetails).Returns(_mockPatientDetailsRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientDetailsController.GetPatientDemoGraphicDetails(27);
            var okResult = actionResult as NotFoundObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }

        [Test]
        public void AddPatientDetails_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            PatientDetails patient = new PatientDetails
            {
                UserId = 27,
                Title = "Ms.",
                FirstName = "Anusha",
                LastName = "C A",
                EmailId = "anusha@mail",
                DOB = new DateTime(1990 - 11 - 11),
                Age = 31,
                ContactNo = "9090909090",
                Gender = "Female",
                Race = "race",
                Ethnicity = "ethnicity",
                Language = "English",
                Address = "Bangalore",
                Emergency_Title = "Mr.",
                Emergency_FirstName = "Chandru",
                Emergency_LastName = "K S",
                Emergency_EmailId = "chandru@mail",
                Emergency_ContactNo = "1010101010",
                Emergency_Relation = "Frienf",
                Emergency_Address = "Bangalore",
                Access_To_Patient_Portal = false,
                Allergy_Details = true,
                Address_Same_As_Patient = true
            };

            _mockPatientDetailsRepo.Setup(x => x.Add(patient));
            _unitOfWork.Setup(x => x.PatientDetails).Returns(_mockPatientDetailsRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientDetailsController.AddPatientDetailsData(patient);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void AddPatientDetails_WhenCalled_ReturnsBadResult()
        {
            //Arrange

            PatientDetails patient = new PatientDetails { };

            _mockPatientDetailsRepo.Setup(x => x.Add(patient));
            _unitOfWork.Setup(x => x.PatientDetails).Returns(_mockPatientDetailsRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientDetailsController.AddPatientDetailsData(patient);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        [Test]
        public void UpdatePatientDetails_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            PatientDetails patient = new PatientDetails
            {
                Patient_Id = 1,
                UserId = 26,
                Title = "Ms.",
                FirstName = "Archana",
                LastName = "C A",
                EmailId = "archana@mail",
                DOB = new DateTime(1990 - 11 - 11),
                Age = 31,
                ContactNo = "9090909090",
                Gender = "Female",
                Race = "race",
                Ethnicity = "ethnicity",
                Language = "fghbn njmk",
                Address = "asxcd nnjiokmk",
                Emergency_Title = "Mr.",
                Emergency_FirstName = "Arun",
                Emergency_LastName = "adfcvbm",
                Emergency_EmailId = "asdx@mail",
                Emergency_ContactNo = "1010101010",
                Emergency_Relation = "Father",
                Emergency_Address = "asxcd nnjiokmk",
                Access_To_Patient_Portal = false,
                Allergy_Details = true,
                Address_Same_As_Patient = true
            };

            _mockPatientDetailsRepo.Setup(x => x.GetById(1)).Returns(patientDetails.FirstOrDefault(x => x.Patient_Id == 1));
            _mockPatientDetailsRepo.Setup(x => x.Update(patient));
            _unitOfWork.Setup(x => x.PatientDetails).Returns(_mockPatientDetailsRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientDetailsController.UpdatePatientDetailsData(1, patient);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void UpdatePatientDetails_WhenCalled_ReturnsBadResult()
        {
            //Arrange

            PatientDetails patient = new PatientDetails
            {
                Patient_Id = 1,
                UserId = 26,
                Title = "Ms.",
                FirstName = "Archana",
                LastName = "C A",
                EmailId = "archana@mail",
                DOB = new DateTime(1990 - 11 - 11),
                Age = 31,
                ContactNo = "9090909090",
                Gender = "Female",
                Race = "race",
                Ethnicity = "ethnicity",
                Language = "fghbn njmk",
                Address = "asxcd nnjiokmk",
                Emergency_Title = "Mr.",
                Emergency_FirstName = "Arun",
                Emergency_LastName = "adfcvbm",
                Emergency_EmailId = "asdx@mail",
                Emergency_ContactNo = "1010101010",
                Emergency_Relation = "Father",
                Emergency_Address = "asxcd nnjiokmk",
                Access_To_Patient_Portal = false,
                Allergy_Details = true,
                Address_Same_As_Patient = true
            };
            _mockPatientDetailsRepo.Setup(x => x.GetById(1)).Returns(patientDetails.FirstOrDefault(x => x.Patient_Id == 1));
            _mockPatientDetailsRepo.Setup(x => x.Update(patient));
            _unitOfWork.Setup(x => x.PatientDetails).Returns(_mockPatientDetailsRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientDetailsController.UpdatePatientDetailsData(2, patient);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }
        private List<PatientDetails> GetListOfPatientDetails()
        {
            return new List<PatientDetails>()
            {
            new PatientDetails {
                    Patient_Id = 1,
                    UserId= 26,
                    Title = "Ms.",
                    FirstName = "Archana",
                    LastName = "C A",
                    EmailId = "archana@mail",
                    DOB = new DateTime(1990-11-11),
                    Age = 31,
                    ContactNo = "9090909090",
                    Gender = "Female",
                    Race = "tyukj",
                    Ethnicity = "asdcbn",
                    Language = "fghbn njmk",
                    Address = "asxcd nnjiokmk",
                    Emergency_Title = "Mr.",
                    Emergency_FirstName = "Arun",
                    Emergency_LastName = "adfcvbm",
                    Emergency_EmailId = "asdx@mail",
                    Emergency_ContactNo = "1010101010",
                    Emergency_Relation = "Father",
                    Emergency_Address = "asxcd nnjiokmk",
                    Access_To_Patient_Portal = false,
                    Allergy_Details = true,
                    Address_Same_As_Patient = true
            }
            };
        }

        private List<Registration> GetListOfPatientDemographicDetails()
        {
            return new List<Registration>()
            {
            new Registration {
                    UserId= 26,
                    Title = "Ms.",
                    FirstName = "Archana",
                    LastName = "C A",
                    EmailId = "archana@mail",
                    DOB = new DateTime(1990-11-11),
                    ContactNo = "9090909090",
                    Gender = "Female", 
            }
            };
        }

    }
}
