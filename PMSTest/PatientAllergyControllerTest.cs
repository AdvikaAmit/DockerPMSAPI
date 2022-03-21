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
    public class PatientAllergyControllerTest
    {
        private Mock<IUnitOfWork> _unitOfWork { get; set; }
        public Mock<IPatientAllergyRepository> _mockPatientAllergyRepo { get; set; }
        PatientAllergyController _patientAllergyController { get; set; }
        IEnumerable<PatientAllergies> patientAllergies;

        [SetUp]
        public void Setup()
        {
            patientAllergies  = GetListOfPatientAllergies().AsEnumerable();
            _mockPatientAllergyRepo = new Mock<IPatientAllergyRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _patientAllergyController = new PatientAllergyController(_unitOfWork.Object);
        }

        [Test]
        public void GetPatientAllergy_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockPatientAllergyRepo.Setup(x => x.GetAll()).Returns(patientAllergies);
            _unitOfWork.Setup(x => x.PatientAllergies).Returns(_mockPatientAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientAllergyController.GetPatientAllergyList();
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetPatientAllergyById_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockPatientAllergyRepo.Setup(x => x.GetById(1)).Returns(patientAllergies.FirstOrDefault(x => x.Patient_Allergy_Id == 1));
            _unitOfWork.Setup(x => x.PatientAllergies).Returns(_mockPatientAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientAllergyController.GetPatientAllergyById(1);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void GetPatientAllergyById_WhenCalled_ReturnsNotFoundBadRequest()
        {
            //Arrange
            _mockPatientAllergyRepo.Setup(x => x.GetById(2)).Returns(patientAllergies.FirstOrDefault(x => x.Patient_Allergy_Id == 2));
            _unitOfWork.Setup(x => x.PatientAllergies).Returns(_mockPatientAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientAllergyController.GetPatientAllergyById(2);
            var okResult = actionResult as NotFoundObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }

        [Test]
        public void GetPatientAllergyDetails_By_UserId_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockPatientAllergyRepo.Setup(x => x.GetPatientAllergyDetails(26)).Returns(patientAllergies.Where(x => x.UserId == 26).ToList());
            _unitOfWork.Setup(x => x.PatientAllergies).Returns(_mockPatientAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientAllergyController.GetPatientAllergyDetails(26);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetPatientAllergyDetails_By_UserId_WhenCalled_ReturnsNotFoundBadRequest()
        {
            //Arrange
            _mockPatientAllergyRepo.Setup(x => x.GetPatientAllergyDetails(27)).Returns(patientAllergies.Where(x => x.UserId == 27).ToList());
            _unitOfWork.Setup(x => x.PatientAllergies).Returns(_mockPatientAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientAllergyController.GetPatientAllergyDetails(27);
            var okResult = actionResult as NotFoundObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }

        [Test]
        public void AddPatientAllergy_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            PatientAllergies[] allergies = new PatientAllergies [] {
                 new PatientAllergies {
                        Allergy_Id = 3,
                        Allergy_Code = "10200112",
                        Allergy_Type = "Hemoglobin Okaloosa",
                        Allergy_Name = "PHIN-VADS",
                        Is_Allergy_Fatal = true,
                        Description = "asdcvn",
                        Clinical_Information = "Hemoglobin",
                        UserId = 27
                   }
            };
                               
            foreach (PatientAllergies patientAllergies in allergies)
            {
                _mockPatientAllergyRepo.Setup(x => x.Add(patientAllergies));
                _unitOfWork.Setup(x => x.PatientAllergies).Returns(_mockPatientAllergyRepo.Object);
                
            }
            // Act
            IActionResult actionResult = (IActionResult)_patientAllergyController.AddPatientAllergyData(allergies);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void AddPatientAllergy_WhenCalled_ReturnsBadResult()
        {
            //Arrange

            PatientAllergies[] allergies = new PatientAllergies[] {};

            foreach (PatientAllergies patientAllergies in allergies)
            {
                _mockPatientAllergyRepo.Setup(x => x.Add(patientAllergies));
                _unitOfWork.Setup(x => x.PatientAllergies).Returns(_mockPatientAllergyRepo.Object);

            }

            // Act
            IActionResult actionResult = (IActionResult)_patientAllergyController.AddPatientAllergyData(allergies);
            var okResult = actionResult as NotFoundObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }
        [Test]
        public void UpdatePatientAllergy_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            PatientAllergies allergies = new PatientAllergies
            {
                Patient_Allergy_Id = 1,
                Allergy_Id = 2,
                Allergy_Code = "10200112",
                Allergy_Type = "Hemoglobin Okaloosa",
                Allergy_Name = "PHIN-VADS",
                Is_Allergy_Fatal = true,
                Description = "asdcvn",
                Clinical_Information = "Hemoglobin",
                UserId = 26
            };

            _mockPatientAllergyRepo.Setup(x => x.GetById(1)).Returns(patientAllergies.FirstOrDefault(x => x.Patient_Allergy_Id == 1));
            _mockPatientAllergyRepo.Setup(x => x.Update(allergies));
            _unitOfWork.Setup(x => x.PatientAllergies).Returns(_mockPatientAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientAllergyController.UpdatePatientAllergyData(1, allergies);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void UpdatePatientDetails_WhenCalled_ReturnsBadResult()
        {
            //Arrange

            PatientAllergies allergies = new PatientAllergies
            {
                Patient_Allergy_Id = 1,
                Allergy_Id = 2,
                Allergy_Code = "10200112",
                Allergy_Type = "Hemoglobin Okaloosa",
                Allergy_Name = "PHIN-VADS",
                Is_Allergy_Fatal = true,
                Description = "asdcvn",
                Clinical_Information = "Hemoglobin",
                UserId = 26
            };
            _mockPatientAllergyRepo.Setup(x => x.GetById(1)).Returns(patientAllergies.FirstOrDefault(x => x.Patient_Allergy_Id == 1));
            _mockPatientAllergyRepo.Setup(x => x.Update(allergies));
            _unitOfWork.Setup(x => x.PatientAllergies).Returns(_mockPatientAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientAllergyController.UpdatePatientAllergyData(2, allergies);
            var okResult = actionResult as NotFoundObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }

        [Test]
        public void DeletePatientAllergyById_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            PatientAllergies allergies = new PatientAllergies
            {
                Patient_Allergy_Id = 1,
                Allergy_Id = 2,
                Allergy_Code = "10200112",
                Allergy_Type = "Hemoglobin Okaloosa",
                Allergy_Name = "PHIN-VADS",
                Is_Allergy_Fatal = true,
                Description = "asdcvn",
                Clinical_Information = "Hemoglobin",
                UserId = 26
            };
            _mockPatientAllergyRepo.Setup(x => x.GetById(1)).Returns(patientAllergies.FirstOrDefault(x => x.Patient_Allergy_Id == 1));
            _mockPatientAllergyRepo.Setup(x => x.Delete(allergies));
            _unitOfWork.Setup(x => x.PatientAllergies).Returns(_mockPatientAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientAllergyController.DeletePatientAllergyData(1);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void DeletePatientAllergyById_WhenCalled_ReturnsBadResult()
        {
            //Arrange
            PatientAllergies allergies = new PatientAllergies
            {
                Patient_Allergy_Id = 1,
                Allergy_Id = 2,
                Allergy_Code = "10200112",
                Allergy_Type = "Hemoglobin Okaloosa",
                Allergy_Name = "PHIN-VADS",
                Is_Allergy_Fatal = true,
                Description = "asdcvn",
                Clinical_Information = "Hemoglobin",
                UserId = 26
            };
            _mockPatientAllergyRepo.Setup(x => x.GetById(1)).Returns(patientAllergies.FirstOrDefault(x => x.Patient_Allergy_Id == 1));
            _mockPatientAllergyRepo.Setup(x => x.Delete(allergies));
            _unitOfWork.Setup(x => x.PatientAllergies).Returns(_mockPatientAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientAllergyController.DeletePatientAllergyData(2);
            var okResult = actionResult as NotFoundObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }

        private List<PatientAllergies> GetListOfPatientAllergies()
        {
            return new List<PatientAllergies>  ()
            {
            new PatientAllergies {
                Patient_Allergy_Id = 1,
                Allergy_Id = 2,
                Allergy_Code = "10200112",
                Allergy_Type = "Hemoglobin Okaloosa",
                Allergy_Name = "PHIN-VADS",
                Is_Allergy_Fatal = true,
                Description = "asdcvn",
                Clinical_Information = "Hemoglobin",
                UserId = 26
            }
            };
        }
    }
}
