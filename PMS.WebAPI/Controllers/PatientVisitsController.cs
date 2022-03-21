using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientVisitsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;        

        public PatientVisitsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;         
        }

        [HttpGet("Visithistory/{userId}")]        
        public IActionResult Get(int userId)
        {
            var visitdata = _unitOfWork.PatientVisits.GetPatientVisitHistoryList(userId);

            return Ok(visitdata);
        }

        [HttpGet("VisithistoryListForPhysician")]
        public IActionResult ListForPhysician()
        {
            var visitdata = _unitOfWork.PatientVisits.GetPatientVisitHistoryListForPhysician();

            return Ok(visitdata);
        }

        [HttpGet("VisitDetail/{id}")]
        public IActionResult GetVisitDetail(int Id)
        {
            var visitdata = _unitOfWork.PatientVisits.GetPatientVisitDetails(Id);

            return Ok(visitdata);
        }

        [HttpPost]
        [Route("AddPatientVisit")]
        public IActionResult AddPatientVisit(PatientVisits visits)
        {
            if(visits.UserId == null)
            {
                return BadRequest("Bad Request");
            }

            int visitId = _unitOfWork.PatientVisits.AddPatientVisit(visits);

            return Ok(visitId);
        }

        [HttpPost]
        [Route("AddPatientDiagnosis")]
        public IActionResult AddPatientDiagnosis(List<PatientVisitDiagnosis> diagnosis)
        {
            if (diagnosis.Count() == 0)
            {
                return BadRequest("Bad Request");
            }

            int visitId = _unitOfWork.PatientVisits.AddVisitDiagnosis(diagnosis);

            return Ok(visitId);
        }

        [HttpPost]
        [Route("AddPatientProcedures")]
        public IActionResult AddPatientProcedures(List<PatientVisitProcedures> procedures)
        {
            if (procedures.Count() == 0)
            {
                return BadRequest("Bad Request");
            }

            int visitId = _unitOfWork.PatientVisits.AddVisitProcedures(procedures);

            return Ok(visitId);
        }

        [HttpPost]
        [Route("AddPatientMedications")]
        public IActionResult AddPatientMedications(List<PatientVisitMedication> medication)
        {
            if (medication.Count() == 0)
            {
                return BadRequest("Bad Request");
            }

            int visitId = _unitOfWork.PatientVisits.AddVisitMedications(medication);

            return Ok(visitId);
        }
    }
}
