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
    public class ProcedureControllerTest
    {
        private Mock<IUnitOfWork> _unitOfWork { get; set; }
        public Mock<IProcedureRepository> _mockProcedureRepo { get; set; }
        ProcedureController _procedureController { get; set; }

        IEnumerable<Procedure> procedures;

        [SetUp]
        public void Setup()
        {
            procedures = GetListOfProcedure().AsEnumerable();
            _mockProcedureRepo = new Mock<IProcedureRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _procedureController = new ProcedureController(_unitOfWork.Object);
        }

        [Test]
        public void GetProcedure_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockProcedureRepo.Setup(x => x.GetAll()).Returns(procedures);
            _unitOfWork.Setup(x => x.Procedures).Returns(_mockProcedureRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_procedureController.GetProcedureList();
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetProcedureById_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockProcedureRepo.Setup(x => x.GetById(1)).Returns(procedures.FirstOrDefault(x => x.Procedure_ID == 1));
            _unitOfWork.Setup(x => x.Procedures).Returns(_mockProcedureRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_procedureController.GetProcedureById(1);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetDiagnosisById_WhenCalled_ReturnsNotFoundBadRequest()
        {
            //Arrange
            _mockProcedureRepo.Setup(x => x.GetById(2)).Returns(procedures.FirstOrDefault(x => x.Procedure_ID == 2));
            _unitOfWork.Setup(x => x.Procedures).Returns(_mockProcedureRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_procedureController.GetProcedureById(1);
            var okResult = actionResult as NotFoundObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(404, okResult.StatusCode);
        }

        [Test]
        public void AddProcedure_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            Procedure procedure = new Procedure
            {
                Procedure_ID = 1,
                Procedure_Code = "10200112",
                Procedure_Description = "PHIN-VADS",
                Procedure_Is_Depricated = false
            };

            _mockProcedureRepo.Setup(x => x.Add(procedure));
            _unitOfWork.Setup(x => x.Procedures).Returns(_mockProcedureRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_procedureController.Addprocedure(procedure);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void AddProcedure_WhenCalled_ReturnsBadResult()
        {
            //Arrange

            Procedure procedure = new Procedure { };

            _mockProcedureRepo.Setup(x => x.Add(procedure));
            _unitOfWork.Setup(x => x.Procedures).Returns(_mockProcedureRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_procedureController.Addprocedure(procedure);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        [Test]
        public void UpdateProcedure_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            Procedure procedure = new Procedure
            {
                Procedure_ID = 1,
                Procedure_Code = "10200112",
                Procedure_Description = "PHIN-VADS",
                Procedure_Is_Depricated = false
            };

            _mockProcedureRepo.Setup(x => x.GetById(1)).Returns(procedures.FirstOrDefault(x => x.Procedure_ID == 1));
            _mockProcedureRepo.Setup(x => x.Update(procedure));
            _unitOfWork.Setup(x => x.Procedures).Returns(_mockProcedureRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_procedureController.UpdateProcedure(1, procedure);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void UpdateProcedure_WhenCalled_ReturnsBadResult()
        {
            //Arrange

            Procedure procedure = new Procedure
            {
                Procedure_ID = 1,
                Procedure_Code = "10200112",
                Procedure_Description = "PHIN-VADS",
                Procedure_Is_Depricated = false
            };

            _mockProcedureRepo.Setup(x => x.GetById(1)).Returns(procedures.FirstOrDefault(x => x.Procedure_ID == 1));
            _mockProcedureRepo.Setup(x => x.Update(procedure));
            _unitOfWork.Setup(x => x.Procedures).Returns(_mockProcedureRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_procedureController.UpdateProcedure(2, procedure);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        private List<Procedure> GetListOfProcedure()
        {
            return new List<Procedure>()
            {
            new Procedure { Procedure_ID = 1, Procedure_Code="10200112", Procedure_Description="PHIN-VADS",Procedure_Is_Depricated=false }
            };
        }
    }
}
