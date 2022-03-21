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
    public class PatientVisitsControllerTest
    {
        private Mock<IUnitOfWork> _unitOfWork { get; set; }
        public Mock<IPatientVisitDataRepository> _mockPatientVisitDataRepo { get; set; }
        PatientVisitsController _patientVisitDataController { get; set; }

        IEnumerable<PatientVisits> patientVisits;

        IEnumerable<PatientVisitsDTO> patientvisitList_Physician;

        PatientVisitDetails patientVisitDetails;

        [SetUp]
        public void Setup()
        {
            patientVisits = GetListOfPatientVisithistory().AsEnumerable();
            patientvisitList_Physician = GetListOfPatientVisitListforPhysician().AsEnumerable();
            patientVisitDetails = GetPatientVisitDetails();
            _mockPatientVisitDataRepo = new Mock<IPatientVisitDataRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _patientVisitDataController = new PatientVisitsController(_unitOfWork.Object);
        }
        [Test]
        public void GetPatientVisitHistoryByUserId_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockPatientVisitDataRepo.Setup(x => x.GetPatientVisitHistoryList(26)).Returns(patientVisits.Where(x => x.UserId == 26).ToList()); 
            _unitOfWork.Setup(x => x.PatientVisits).Returns(_mockPatientVisitDataRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientVisitDataController.Get(26);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetPatientVisitHistoryForPhysician_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockPatientVisitDataRepo.Setup(x => x.GetPatientVisitHistoryListForPhysician()).Returns((List<PatientVisitsDTO>)patientvisitList_Physician);
            _unitOfWork.Setup(x => x.PatientVisits).Returns(_mockPatientVisitDataRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientVisitDataController.ListForPhysician();
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void GetPatientVisitDetailById_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            _mockPatientVisitDataRepo.Setup(x => x.GetPatientVisitDetails(1));
            _unitOfWork.Setup(x => x.PatientVisits).Returns(_mockPatientVisitDataRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientVisitDataController.GetVisitDetail(1);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void AddPatientVisitDetails_WhenCalled_ReturnsOkResult()
        {
            //Arrange

            PatientVisits visits = new PatientVisits
            {
                Visit_Id = 2,
                Visit_Date = new DateTime(2022 - 02 - 23),
                Height = "90",
                Weight = "40",
                Blood_Pressure = "56",
                Body_Temperature = "45",
                Respiration_Rate = "45",
                UserId = 27,
            };

            _mockPatientVisitDataRepo.Setup(x => x.AddPatientVisit(visits));
            _unitOfWork.Setup(x => x.PatientVisits).Returns(_mockPatientVisitDataRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientVisitDataController.AddPatientVisit(visits);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void AddPatientVisitDetails_WhenCalled_ReturnsBadResult()
        {
            //Arrange

            PatientVisits visits = new PatientVisits{ };

            _mockPatientVisitDataRepo.Setup(x => x.AddPatientVisit(visits));
            _unitOfWork.Setup(x => x.PatientVisits).Returns(_mockPatientVisitDataRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientVisitDataController.AddPatientVisit(visits);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        [Test]
        public void AddPatientDiagnosisDetails_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            List<PatientVisitDiagnosis> diagnoses = new List<PatientVisitDiagnosis>()
            {
                new PatientVisitDiagnosis {
                Visit_Id = 2,
                Diagnosis_Id = 70,
                Description = null,
                Note =  null
            }
            };
            _mockPatientVisitDataRepo.Setup(x => x.AddVisitDiagnosis(diagnoses));
            _unitOfWork.Setup(x => x.PatientVisits).Returns(_mockPatientVisitDataRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientVisitDataController.AddPatientDiagnosis(diagnoses);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void AddPatientDiagnosisDetails_WhenCalled_ReturnsBadResult()
        {
            //Arrange
            List<PatientVisitDiagnosis> diagnoses = new List<PatientVisitDiagnosis>(){};
            _mockPatientVisitDataRepo.Setup(x => x.AddVisitDiagnosis(diagnoses));
            _unitOfWork.Setup(x => x.PatientVisits).Returns(_mockPatientVisitDataRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientVisitDataController.AddPatientDiagnosis(diagnoses);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        [Test]
        public void AddPatientProcedureDetails_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            List<PatientVisitProcedures> procedure = new List<PatientVisitProcedures>()
            {
                new PatientVisitProcedures {
                Visit_Id = 2,
                Procedure_Id = 70,
                Description = null,
                Note =  null
            }
            };
            _mockPatientVisitDataRepo.Setup(x => x.AddVisitProcedures(procedure));
            _unitOfWork.Setup(x => x.PatientVisits).Returns(_mockPatientVisitDataRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientVisitDataController.AddPatientProcedures(procedure);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void AddPatientProcedureDetails_WhenCalled_ReturnsBadResult()
        {
            //Arrange
            List<PatientVisitProcedures> procedure = new List<PatientVisitProcedures>() { };
            _mockPatientVisitDataRepo.Setup(x => x.AddVisitProcedures(procedure));
            _unitOfWork.Setup(x => x.PatientVisits).Returns(_mockPatientVisitDataRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientVisitDataController.AddPatientProcedures(procedure);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }

        [Test]
        public void AddPatientMedicationDetails_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            List<PatientVisitMedication> medications = new List<PatientVisitMedication>()
            {
                new PatientVisitMedication {
                    Visit_Id = 2,
                    Drug_Id = 5,
                    Dosage = "50",
                    Description = null,
                    Note = null
                
            }
            };
            _mockPatientVisitDataRepo.Setup(x => x.AddVisitMedications(medications));
            _unitOfWork.Setup(x => x.PatientVisits).Returns(_mockPatientVisitDataRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientVisitDataController.AddPatientMedications(medications);
            var okResult = actionResult as OkObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void AddPatientMedicationDetails_WhenCalled_ReturnsBadResult()
        {
            //Arrange
            List<PatientVisitMedication> medications = new List<PatientVisitMedication>() { };
            _mockPatientVisitDataRepo.Setup(x => x.AddVisitMedications(medications));
            _unitOfWork.Setup(x => x.PatientVisits).Returns(_mockPatientVisitDataRepo.Object);

            // Act
            IActionResult actionResult = (IActionResult)_patientVisitDataController.AddPatientMedications(medications);
            var okResult = actionResult as BadRequestObjectResult;

            // Assert the result
            Assert.IsNotNull(okResult);
            Assert.AreEqual(400, okResult.StatusCode);
        }
        private List<PatientVisits> GetListOfPatientVisithistory()
        {
            return new List<PatientVisits>()
            {
            new PatientVisits {
                Visit_Id = 1,
                Visit_Date = new DateTime(2022-02-23),
                Height = "90",
                Weight = "40",
                Blood_Pressure = "56",
                Body_Temperature = "45",
                Respiration_Rate = "45",
                UserId = 26,
            }
            };
        }

        private List<PatientVisitsDTO> GetListOfPatientVisitListforPhysician()
        {
            return new List<PatientVisitsDTO>()
            {
            new PatientVisitsDTO {
                Visit_Id = 1,
                Visit_Date = new DateTime(2022-02-23),
                Height = "90",
                Weight = "40",
                Blood_Pressure = "56",
                Body_Temperature = "45",
                Respiration_Rate = "45",
                UserId = 26,
            }
            };
        }

        private PatientVisitDetails GetPatientVisitDetails()
        {
            PatientVisitDetails visitDetails = new PatientVisitDetails();
            visitDetails.patientVisits = new PatientVisits {
                Visit_Id = 1,
                Visit_Date = new DateTime(2022 - 02 - 23),
                Height = "90",
                Weight = "40",
                Blood_Pressure = "56",
                Body_Temperature = "45",
                Respiration_Rate = "45",
                UserId = 26,

            };
            visitDetails.diagnoses = new List<PatientVisitDiagnosisDTO>() { 
                new PatientVisitDiagnosisDTO
                {
                    Id = 1,
                    Visit_Id = 1,
                    Diagnosis_Id = 70,
                    Diagnosis_Code = "C9150",
                    Diagnosis_Description = "Adult T-cell lymph/leuk (HTLV-1-assoc) not achieve remission",
                    Diagnosis_Is_Depricated = null,
                    Description = null,
                    Note =  null
                }
            };
            visitDetails.procedures = new List<PatientVisitProceduresDTO>() { 
                new PatientVisitProceduresDTO
                {
                    Id = 1,
                    Visit_Id = 1,
                    Procedure_Id = 3,
                    Procedure_Code = "16072",
                    Procedure_Description = "Bypass Cerebral Ventricle to Atrium with Autologous Tissue Substitute, Open Approach",
                    Procedure__Is_Depricated = null,
                    Description = null,
                    Note = null
                }
            };
            visitDetails.medications = new List<PatientVisitMedicationDTO>() { 
                new PatientVisitMedicationDTO
                {
                    Id = 1,
                    Visit_Id = 1,
                    Drug_Id = 9,
                    Drug_Name = "SampleDrug",
                    Drug_Generic_Name = "Sample",
                    Drug_Manufacture_Name = null,
                    Drug_Form = "pawder",
                    Drug_Strength = null,
                    Dosage = "50",
                    Description = null,
                    Note = null
                }
            };

            return visitDetails;
               
        }


    }
}
