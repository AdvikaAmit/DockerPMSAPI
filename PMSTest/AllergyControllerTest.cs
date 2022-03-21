using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PMS.DAL;
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
using System.Web.Http;
using System.Web.Http.Results;



namespace PMSTest
{
    [TestFixture]
    public class AllergyControllerTest
    {
        private Mock<IUnitOfWork> _unitOfWork { get; set; }
        public Mock<IAllergyRepository> _mockAllergyRepo { get; set; }
        AllergyController _allergyController { get; set; }

        IEnumerable<Allergy> allergies;


        [SetUp]
        public void Setup()
        {
            allergies = GetListOfAllergy().AsEnumerable();
            _mockAllergyRepo = new Mock<IAllergyRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _allergyController = new AllergyController(_unitOfWork.Object);
        }

        [Test]
        public void GetAllergy_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockAllergyRepo.Setup(x => x.GetAll()).Returns(allergies);
            _unitOfWork.Setup(x => x.Allergies).Returns(_mockAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_allergyController.GetAllergy();
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);            
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        [Ignore("GetAllergy_WhenCalled_ReturnsAllItems")]
        public void GetAllergy_WhenCalled_ReturnsAllItems()
        {
            var st = _unitOfWork.Setup(x => x.Allergies);

            //Arrange
            _mockAllergyRepo.Setup(x => x.GetAll()).Returns(allergies);
            _unitOfWork.Setup(x => x.Allergies).Returns(_mockAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_allergyController.GetAllergy();
            var okResult = actionResult as OkObjectResult;

            // Assert the result            
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);           
        }


        [Test]
        public void GetAllergyById_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockAllergyRepo.Setup(x => x.GetById(1)).Returns(allergies.FirstOrDefault(x=>x.Allergy_Id == 1));
            _unitOfWork.Setup(x => x.Allergies).Returns(_mockAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_allergyController.GetAllergyById(1);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);            
        }

        [Test]
        public void GetAllergyById_WhenCalled_ReturnsNotFoundBadRequest()
        {
            //Arrange
            _mockAllergyRepo.Setup(x => x.GetById(2)).Returns(allergies.FirstOrDefault(x => x.Allergy_Id == 2));
            _unitOfWork.Setup(x => x.Allergies).Returns(_mockAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_allergyController.GetAllergyById(1);
            var okResult = actionResult as NotFoundObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }

        [Test]
        public void AddAllergy_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            Allergy allergy = new Allergy 
            { 
                Allergy_Code="0003",
                Allergy_Name="Allergy0003",
                Allergy_Clinical_Information= "Allergy0003",
                Allergy_Description="desc",
                Allergy_Source="Lab", Allergy_Type="type"
            };

            _mockAllergyRepo.Setup(x => x.Add(allergy));
            _unitOfWork.Setup(x => x.Allergies).Returns(_mockAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_allergyController.AddAllergy(allergy);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void AddAllergy_WhenCalled_ReturnsBadResult()
        {
            //Arrange

            Allergy allergy = new Allergy { };

            _mockAllergyRepo.Setup(x => x.Add(allergy));
            _unitOfWork.Setup(x => x.Allergies).Returns(_mockAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_allergyController.AddAllergy(allergy);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        [Test]
        public void UpdateAllergy_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            Allergy allergy = new Allergy
            {
                Allergy_Id = 1,
                Allergy_Code = "10200112",
                Allergy_Type = "PHIN-VADS",
                Allergy_Name = "Hemoglobin Okaloosa",
                Allergy_Clinical_Information = "Hemoglobin",
                Allergy_Description = "",
                Allergy_Source = "FHIR",
                Allerginicity = "Allerginicity1"
            };

            _mockAllergyRepo.Setup(x => x.GetById(1)).Returns(allergies.FirstOrDefault(x => x.Allergy_Id == 1));            
            _mockAllergyRepo.Setup(x => x.Update(allergy));
            _unitOfWork.Setup(x => x.Allergies).Returns(_mockAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_allergyController.UpdateAllergy(1,allergy);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void UpdateAllergy_WhenCalled_ReturnsBadResult()
        {
            //Arrange

            Allergy allergy = new Allergy
            {
                Allergy_Id = 1,
                Allergy_Code = "10200112",
                Allergy_Type = "PHIN-VADS",
                Allergy_Name = "Hemoglobin Okaloosa",
                Allergy_Clinical_Information = "Hemoglobin",
                Allergy_Description = "",
                Allergy_Source = "FHIR",
                Allerginicity = "Allerginicity1"
            };

            _mockAllergyRepo.Setup(x => x.GetById(1)).Returns(allergies.FirstOrDefault(x => x.Allergy_Id == 1));
            _mockAllergyRepo.Setup(x => x.Update(allergy));
            _unitOfWork.Setup(x => x.Allergies).Returns(_mockAllergyRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_allergyController.UpdateAllergy(2,allergy);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        private List<Allergy> GetListOfAllergy()
        {
            return new List<Allergy>()
            {
            new Allergy { Allergy_Id = 1, Allergy_Code="10200112", Allergy_Type="PHIN-VADS",Allergy_Name="Hemoglobin Okaloosa",Allergy_Clinical_Information="Hemoglobin",
                          Allergy_Description="",Allergy_Source="FHIR", Allerginicity = "Allerginicity1" }
            };
        }
    }
}