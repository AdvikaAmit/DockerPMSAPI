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
    public class DiagnosisControllerTest
    {
        private Mock<IUnitOfWork> _unitOfWork { get; set; }
        public Mock<IDiagnosisRepository> _mockDiagnosisRepo { get; set; }
        DiagnosisController _diagnosisController { get; set; }

        IEnumerable<Diagnosis> diagnoses;

        [SetUp]
        public void Setup()
        {
            diagnoses = GetListOfDiagnosis().AsEnumerable();
            _mockDiagnosisRepo = new Mock<IDiagnosisRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _diagnosisController = new DiagnosisController(_unitOfWork.Object);
        }

        [Test]
        public void GetDiagnosis_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockDiagnosisRepo.Setup(x => x.GetAll()).Returns(diagnoses);
            _unitOfWork.Setup(x => x.Diagnosis).Returns(_mockDiagnosisRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_diagnosisController.GetDiagnosisList();
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }


        [Test]
        public void GetDiagnosisById_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockDiagnosisRepo.Setup(x => x.GetById(1)).Returns(diagnoses.FirstOrDefault(x => x.Diagnosis_Id == 1));
            _unitOfWork.Setup(x => x.Diagnosis).Returns(_mockDiagnosisRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_diagnosisController.GetDiagnosisById(1);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetDiagnosisById_WhenCalled_ReturnsNotFoundBadRequest()
        {
            //Arrange
            _mockDiagnosisRepo.Setup(x => x.GetById(2)).Returns(diagnoses.FirstOrDefault(x => x.Diagnosis_Id == 2));
            _unitOfWork.Setup(x => x.Diagnosis).Returns(_mockDiagnosisRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_diagnosisController.GetDiagnosisById(2);
            var okResult = actionResult as NotFoundObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }

        [Test]
        public void AddDiagnosis_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            Diagnosis diagnosis = new Diagnosis
            {
                Diagnosis_Id = 1,
                Diagnosis_Code = "0001",
                Diagnosis_Description = "Diagnosis 0001",
                Diagnosis_Is_Depricated = true
            };

            _mockDiagnosisRepo.Setup(x => x.Add(diagnosis));
            _unitOfWork.Setup(x => x.Diagnosis).Returns(_mockDiagnosisRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_diagnosisController.AddDiagnosis(diagnosis);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void AddDiagnosis_WhenCalled_ReturnsBadResult()
        {
            //Arrange

            Diagnosis diagnosis = new Diagnosis { };

            _mockDiagnosisRepo.Setup(x => x.Add(diagnosis));
            _unitOfWork.Setup(x => x.Diagnosis).Returns(_mockDiagnosisRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_diagnosisController.AddDiagnosis(diagnosis);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        [Test]
        public void UpdateDiagnosis_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            Diagnosis diagnosis = new Diagnosis
            {
                Diagnosis_Id = 1,
                Diagnosis_Code = "0001",
                Diagnosis_Description = "Diagnosis 0001",
                Diagnosis_Is_Depricated = true
            };

            _mockDiagnosisRepo.Setup(x => x.GetById(1)).Returns(diagnoses.FirstOrDefault(x => x.Diagnosis_Id == 1));
            _mockDiagnosisRepo.Setup(x => x.Update(diagnosis));
            _unitOfWork.Setup(x => x.Diagnosis).Returns(_mockDiagnosisRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_diagnosisController.UpdateDiagnosis(1, diagnosis);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void UpdateDiagnosis_WhenCalled_ReturnsBadResult()
        {
            //Arrange

            Diagnosis diagnosis = new Diagnosis
            {
                Diagnosis_Id = 1,
                Diagnosis_Code = "0001",
                Diagnosis_Description = "Diagnosis 0001",
                Diagnosis_Is_Depricated = true
            };

            _mockDiagnosisRepo.Setup(x => x.GetById(1)).Returns(diagnoses.FirstOrDefault(x => x.Diagnosis_Id == 1));
            _mockDiagnosisRepo.Setup(x => x.Update(diagnosis));
            _unitOfWork.Setup(x => x.Diagnosis).Returns(_mockDiagnosisRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_diagnosisController.UpdateDiagnosis(2, diagnosis);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        private List<Diagnosis> GetListOfDiagnosis()
        {
            return new List<Diagnosis>()
            {
            new Diagnosis { Diagnosis_Id = 1, Diagnosis_Code="0001", Diagnosis_Description="Diagnosis 0001",Diagnosis_Is_Depricated=true }
            };
        }
    }
}
