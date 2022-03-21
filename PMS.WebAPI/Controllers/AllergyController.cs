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
    public class AllergyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AllergyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllergy()
        {
            var allergies = _unitOfWork.Allergies.GetAll();
            return Ok(allergies);
        }

        [HttpGet("{id}")]
        public IActionResult GetAllergyById(int id)
        {
            var allergy = _unitOfWork.Allergies.GetById(id);
            if(allergy == null)
            {
                return NotFound("Allergy data not found");
            }
            return Ok(allergy);
        }

        [HttpPost]
        public IActionResult AddAllergy(Allergy allergy )
        {
            if(allergy.Allergy_Name == null)
            {
                return BadRequest("Bad Request");
            }

            _unitOfWork.Allergies.Add(allergy);
            _unitOfWork.Complete();
            return Ok(allergy.Allergy_Id);
        }

        [HttpPut]
        public IActionResult UpdateAllergy(int Id,Allergy allergy)
        {
            if (allergy.Allergy_Id != Id)
            {
                return BadRequest("Bad Request");
            }

            var allergydata = _unitOfWork.Allergies.GetById(Id);

            if(allergydata == null)
            {
                return NotFound("Allergy Data not found");
            }

            allergydata.Allergy_Code = allergy.Allergy_Code;
            allergydata.Allergy_Type = allergy.Allergy_Type;
            allergydata.Allergy_Name = allergy.Allergy_Name;
            allergydata.Allergy_Description = allergy.Allergy_Description;
            allergydata.Allergy_Clinical_Information = allergy.Allergy_Clinical_Information;
            allergydata.Allergy_Source = allergy.Allergy_Source;
            allergydata.Allerginicity = allergy.Allerginicity;

            _unitOfWork.Allergies.Update(allergydata);
            _unitOfWork.Complete();
            return Ok(allergy.Allergy_Id);
        }
    }
}
