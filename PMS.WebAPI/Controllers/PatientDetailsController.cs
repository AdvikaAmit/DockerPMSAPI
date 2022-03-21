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
    public class PatientDetailsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PatientDetailsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetPatientDetailList()
        {
            var patientDetails = _unitOfWork.PatientDetails.GetAll();
            return Ok(patientDetails);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetPatientDetailById(int id)
        {
            var patientDetails = _unitOfWork.PatientDetails.GetById(id);
            if (patientDetails == null)
            {
                return NotFound("data not found");
            }
            return Ok(patientDetails);
        }

        
        //[Route("GetPatientDemoGraphicDetails")]
        [HttpGet("GetPatientDemoGraphicDetails/{id}")]
        public IActionResult GetPatientDemoGraphicDetails(int id)
        {
            
            var patientDetails = _unitOfWork.PatientDetails.GetPatientDemographicDetails(id);
            if (patientDetails == null)
            {
                return NotFound("data not found");
            }
            return Ok(patientDetails);
        }

        [HttpPost]
        public IActionResult AddPatientDetailsData(PatientDetails patientDetails)
        {
            if (patientDetails.FirstName == null)
            {
                return BadRequest("Bad Request");
            }

            try
            {
                _unitOfWork.PatientDetails.Add(patientDetails);
                _unitOfWork.Complete();
                return Ok(patientDetails.Patient_Id);
            }
            catch(Exception ex)
            {                
                return BadRequest(ex.Message);
            }            
        }

        [HttpPut]
        public IActionResult UpdatePatientDetailsData(int Id, PatientDetails patientDetails)
        {
            if (patientDetails.Patient_Id != Id)
            {
                return BadRequest("Bad Request");
            }

            var data = _unitOfWork.PatientDetails.GetById(Id);

            if (data == null)
            {
                return NotFound("Data not found");
            }

            data.Patient_Id = patientDetails.Patient_Id;
            data.UserId = patientDetails.UserId;
            data.Title = patientDetails.Title;
            data.FirstName = patientDetails.FirstName;
            data.LastName = patientDetails.LastName;
            data.EmailId = patientDetails.EmailId;
            data.DOB = patientDetails.DOB;
            data.ContactNo = patientDetails.ContactNo;
            data.Gender = patientDetails.Gender;
            data.Race = patientDetails.Race;
            data.Ethnicity = patientDetails.Ethnicity;
            data.Language = patientDetails.Language;
            data.Address = patientDetails.Address;
            data.Emergency_Title = patientDetails.Emergency_Title;
            data.Emergency_FirstName = patientDetails.Emergency_FirstName;
            data.Emergency_LastName = patientDetails.Emergency_LastName;
            data.Emergency_EmailId = patientDetails.Emergency_EmailId;
            data.Emergency_Relation = patientDetails.Emergency_Relation;
            data.Emergency_ContactNo = patientDetails.Emergency_ContactNo;
            data.Emergency_Address = patientDetails.Emergency_Address;
            data.Access_To_Patient_Portal = patientDetails.Access_To_Patient_Portal;
            data.Allergy_Details = patientDetails.Allergy_Details;
            data.Address_Same_As_Patient = patientDetails.Address_Same_As_Patient;
            
            _unitOfWork.PatientDetails.Update(data);
            _unitOfWork.Complete();
            return Ok(data.Patient_Id);
        }
    }
}
