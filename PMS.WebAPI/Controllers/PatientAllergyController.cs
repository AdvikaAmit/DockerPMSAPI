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
    public class PatientAllergyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PatientAllergyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetPatientAllergyList()
        {
            var patientAllergy = _unitOfWork.PatientAllergies.GetAll();
            return Ok(patientAllergy);
        }
        [HttpGet("{id}")]
        public IActionResult GetPatientAllergyById(int id)
        {
            var patientAllergy = _unitOfWork.PatientAllergies.GetById(id);
            if (patientAllergy == null)
            {
                return NotFound("data not found");
            }
            return Ok(patientAllergy);
        }

        [HttpGet("GetPatientAllergyDetails/{id}")]
        public IActionResult GetPatientAllergyDetails(int id)
        {
            var patientAllergy = _unitOfWork.PatientAllergies.GetPatientAllergyDetails(id);
            if (patientAllergy.Count == 0)
            {
                return NotFound("data not found");
            }
            return Ok(patientAllergy);
        }

        [HttpPost]
        public IActionResult AddPatientAllergyData(PatientAllergies[] patientAllergy)
        {
            if (patientAllergy.Length == 0)
            {
                return NotFound("Bad Request");
            }

            var data = _unitOfWork.PatientAllergies.GetAll();
            data = data.Where(d => d.UserId == patientAllergy.FirstOrDefault().UserId).ToList();

            foreach (var item in data)
            {
                _unitOfWork.PatientAllergies.Delete(item);
                _unitOfWork.Complete();
            }

            foreach (PatientAllergies patientAllergies in patientAllergy)
            {
                patientAllergies.Patient_Allergy_Id = 0;
                _unitOfWork.PatientAllergies.Add(patientAllergies);
                _unitOfWork.Complete();
            }
            
            return Ok(patientAllergy);
        }

        [HttpPut]
        public IActionResult UpdatePatientAllergyData(int Id, PatientAllergies patientAllergy)
        {
            if (patientAllergy == null)
            {
                return BadRequest();
            }

            var data = _unitOfWork.PatientAllergies.GetById(Id);

            if (data == null)
            {
                return NotFound("Data not found");
            }

            data.Patient_Allergy_Id = patientAllergy.Patient_Allergy_Id;
            //data.Patient_Id = patientAllergy.Patient_Id;
            data.UserId = patientAllergy.UserId;
            data.Allergy_Id = patientAllergy.Allergy_Id;
            data.Allergy_Code = patientAllergy.Allergy_Code;
            data.Allergy_Name = patientAllergy.Allergy_Name;
            data.Allergy_Type = patientAllergy.Allergy_Type;
            data.Description = patientAllergy.Description;
            data.Clinical_Information = patientAllergy.Clinical_Information;
            data.Is_Allergy_Fatal = patientAllergy.Is_Allergy_Fatal;
            
            _unitOfWork.PatientAllergies.Update(data);
            _unitOfWork.Complete();
            return Ok(data.Patient_Allergy_Id);
        }
        [HttpDelete]
        public IActionResult DeletePatientAllergyData(int Id)
        {

            var data = _unitOfWork.PatientAllergies.GetById(Id);

            if (data == null)
            {
                return NotFound("Data not found");
            }

            _unitOfWork.PatientAllergies.Delete(data);
            _unitOfWork.Complete();
            return Ok(data.Patient_Allergy_Id);

        }
    }
}

