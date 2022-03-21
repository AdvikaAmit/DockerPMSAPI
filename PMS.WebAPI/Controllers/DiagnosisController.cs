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
    public class DiagnosisController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DiagnosisController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetDiagnosisList()
        {
            var diagnosis = _unitOfWork.Diagnosis.GetAll();
            return Ok(diagnosis);
        }

        [HttpGet("{id}")]
        public IActionResult GetDiagnosisById(int id)
        {
            var diagnosis = _unitOfWork.Diagnosis.GetById(id);
            if (diagnosis == null)
            {
                return NotFound("Not Found");
            }
            return Ok(diagnosis);
        }

        [HttpPost]
        public IActionResult AddDiagnosis(Diagnosis diagnosis)
        {
            if (diagnosis.Diagnosis_Code == null)
            {
                return BadRequest("Bad Request");
            }

            _unitOfWork.Diagnosis.Add(diagnosis);
            _unitOfWork.Complete();
            return Ok(diagnosis.Diagnosis_Id);
        }

        [HttpPut]
        public IActionResult UpdateDiagnosis(int Id, Diagnosis diagnosis)
        {
            if (Id != diagnosis.Diagnosis_Id)
            {
                return BadRequest("Bad Request");
            }

            var data = _unitOfWork.Diagnosis.GetById(Id);

            if (data == null)
            {
                return NotFound("Data not found");
            }

            data.Diagnosis_Id = diagnosis.Diagnosis_Id;
            data.Diagnosis_Code = diagnosis.Diagnosis_Code;
            data.Diagnosis_Description = diagnosis.Diagnosis_Description;
            data.Diagnosis_Is_Depricated = diagnosis.Diagnosis_Is_Depricated;           

            _unitOfWork.Diagnosis.Update(data);
            _unitOfWork.Complete();
            return Ok(diagnosis.Diagnosis_Id);
        }
    }
}
