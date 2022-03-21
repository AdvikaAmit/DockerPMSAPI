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
    public class NotesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetNotes()
        {
            var notes = _unitOfWork.Notes.GetAll();
            return Ok(notes);
        }

        [HttpGet("SentNotes/{userId}")]
        public IActionResult GetSendNotes(int userId)
        {            
            var notes = _unitOfWork.Notes.SentNotes(userId);
            return Ok(notes);
        }

        [HttpGet("ReceivedNotes/{userId}")]
        public IActionResult ReceivedNotes(int userId)
        {
            var notes = _unitOfWork.Notes.ReceivedNotes(userId);
            return Ok(notes);
        }

        [HttpPost("SendNote")]
        public IActionResult AddNote(Notes note)
        {
            if (note == null)
            {
                return BadRequest("Bad Request");
            }

            _unitOfWork.Notes.Add(note);
            _unitOfWork.Complete();
            return Ok(note.Id);
        }
    }
}
