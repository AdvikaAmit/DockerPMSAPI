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
    public class ProcedureController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProcedureController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetProcedureList()
        {
            var procedures = _unitOfWork.Procedures.GetAll();
            return Ok(procedures);
        }

        [HttpGet("{id}")]
        public IActionResult GetProcedureById(int id)
        {
            var procedure = _unitOfWork.Procedures.GetById(id);
            if (procedure == null)
            {
                return NotFound("data not found");
            }
            return Ok(procedure);
        }

        [HttpPost]
        public IActionResult Addprocedure(Procedure procedure)
        {
            if (procedure.Procedure_Code == null)
            {
                return BadRequest("Bad Request");
            }

            _unitOfWork.Procedures.Add(procedure);
            _unitOfWork.Complete();
            return Ok(procedure.Procedure_ID);
        }

        [HttpPut]
        public IActionResult UpdateProcedure(int Id, Procedure procedure)
        {
            if (procedure.Procedure_ID != Id)
            {
                return BadRequest("Bad Request");
            }

            var proceduredata = _unitOfWork.Procedures.GetById(Id);

            if (proceduredata == null)
            {
                return NotFound("procedure Data not found");
            }

            proceduredata.Procedure_ID = procedure.Procedure_ID;
            proceduredata.Procedure_Code = procedure.Procedure_Code;
            proceduredata.Procedure_Description = procedure.Procedure_Description;
            proceduredata.Procedure_Is_Depricated = procedure.Procedure_Is_Depricated;            

            _unitOfWork.Procedures.Update(proceduredata);
            _unitOfWork.Complete();
            return Ok(proceduredata.Procedure_ID);
        }
    }
}
