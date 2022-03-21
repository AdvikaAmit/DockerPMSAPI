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
    public class DrugDataController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DrugDataController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetDrugList()
        {
            var drugs = _unitOfWork.DrugDatas.GetAll();
            return Ok(drugs);
        }

        [HttpGet("{id}")]
        public IActionResult GetDrugById(int id)
        {
            var drugs = _unitOfWork.DrugDatas.GetById(id);
            if (drugs == null)
            {
                return NotFound("data not found");
            }
            return Ok(drugs);
        }

        [HttpPost]
        public IActionResult AddDrugData(DrugData drug)
        {
            if (drug.Drug_Name == null)
            {
                return BadRequest("Bad Request");
            }

            _unitOfWork.DrugDatas.Add(drug);
            _unitOfWork.Complete();
            return Ok(drug.Drug_ID);
        }

        [HttpPut]
        public IActionResult UpdateDrug(int Id, DrugData drug)
        {
            if (drug.Drug_ID != Id)
            {
                return BadRequest("Bad Request");
            }

            var data = _unitOfWork.DrugDatas.GetById(Id);

            if (data == null)
            {
                return NotFound("Data not found");
            }

            data.Drug_ID = drug.Drug_ID;
            data.Drug_Name = drug.Drug_Name;
            data.Drug_Generic_Name = drug.Drug_Generic_Name;
            data.Drug_Manufacture_Name = drug.Drug_Manufacture_Name;
            data.Drug_Form = drug.Drug_Form;
            data.Drug_Strength = drug.Drug_Strength;

            _unitOfWork.DrugDatas.Update(data);
            _unitOfWork.Complete();
            return Ok(data.Drug_ID);
        }
    }
}
