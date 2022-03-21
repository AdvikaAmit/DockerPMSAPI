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
    public class DrugDataControllerTest
    {
        private Mock<IUnitOfWork> _unitOfWork { get; set; }
        public Mock<IDrugDataRepository> _mockdrugRepo { get; set; }
        DrugDataController _drugDataController { get; set; }

        IEnumerable<DrugData> drugDatas;

        [SetUp]
        public void Setup()
        {
            drugDatas = GetListOfDrugData().AsEnumerable();
            _mockdrugRepo = new Mock<IDrugDataRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _drugDataController = new DrugDataController(_unitOfWork.Object);
        }

        [Test]
        public void GetDrugData_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockdrugRepo.Setup(x => x.GetAll()).Returns(drugDatas);
            _unitOfWork.Setup(x => x.DrugDatas).Returns(_mockdrugRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_drugDataController.GetDrugList();
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetDrugDataById_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockdrugRepo.Setup(x => x.GetById(1)).Returns(drugDatas.FirstOrDefault(x => x.Drug_ID == 1));
            _unitOfWork.Setup(x => x.DrugDatas).Returns(_mockdrugRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_drugDataController.GetDrugById(1);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetDrugDataById_WhenCalled_ReturnsNotFoundBadRequest()
        {
            //Arrange
            _mockdrugRepo.Setup(x => x.GetById(2)).Returns(drugDatas.FirstOrDefault(x => x.Drug_ID == 2));
            _unitOfWork.Setup(x => x.DrugDatas).Returns(_mockdrugRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_drugDataController.GetDrugById(2);
            var okResult = actionResult as NotFoundObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }

        [Test]
        public void AddDrugData_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            DrugData drugData = new DrugData
            {                
                Drug_Name = "Aripiprazolesss",
                Drug_Form = "liquids form",
                Drug_Generic_Name = "Aripiprazole",
                Drug_Manufacture_Name = "USA.gov",
                Drug_Strength = "100"
            };

            _mockdrugRepo.Setup(x => x.Add(drugData));
            _unitOfWork.Setup(x => x.DrugDatas).Returns(_mockdrugRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_drugDataController.AddDrugData(drugData);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void AddDrugData_WhenCalled_ReturnsBadResult()
        {
            //Arrange

            DrugData drugData = new DrugData {};

            _mockdrugRepo.Setup(x => x.Add(drugData));
            _unitOfWork.Setup(x => x.DrugDatas).Returns(_mockdrugRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_drugDataController.AddDrugData(drugData);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        [Test]
        public void UpdateDrugData_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            DrugData drugData = new DrugData
            {
                Drug_ID = 1,
                Drug_Name = "Aripiprazolesss",
                Drug_Form = "liquids form",
                Drug_Generic_Name = "Aripiprazole",
                Drug_Manufacture_Name = "USA.gov",
                Drug_Strength = "100"
            };

            _mockdrugRepo.Setup(x => x.GetById(1)).Returns(drugDatas.FirstOrDefault(x => x.Drug_ID == 1));
            _mockdrugRepo.Setup(x => x.Update(drugData));
            _unitOfWork.Setup(x => x.DrugDatas).Returns(_mockdrugRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_drugDataController.UpdateDrug(1, drugData);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void UpdateDrugData_WhenCalled_ReturnsBadResult()
        {
            //Arrange

            DrugData drugData = new DrugData
            {
                Drug_ID = 1,
                Drug_Name = "Aripiprazolesss",
                Drug_Form = "liquids form",
                Drug_Generic_Name = "Aripiprazole",
                Drug_Manufacture_Name = "USA.gov",
                Drug_Strength = "100"
            };

            _mockdrugRepo.Setup(x => x.GetById(1)).Returns(drugDatas.FirstOrDefault(x => x.Drug_ID == 1));
            _mockdrugRepo.Setup(x => x.Update(drugData));
            _unitOfWork.Setup(x => x.DrugDatas).Returns(_mockdrugRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_drugDataController.UpdateDrug(2, drugData);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        private List<DrugData> GetListOfDrugData()
        {
            return new List<DrugData>()
            {
            new DrugData { Drug_ID = 1, Drug_Name="Aripiprazolesss",Drug_Form="liquids form",Drug_Generic_Name="Aripiprazole",Drug_Manufacture_Name="USA.gov",Drug_Strength="100" }
            };
        }
    }
}
