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
    public class NotesControllerTest
    {
        private Mock<IUnitOfWork> _unitOfWork { get; set; }
        private Mock<INotesRepository> _mockNotesRepo { get; set; }
        NotesController _notesController { get; set; }
        IEnumerable<Notes> notes { get; set; }
        List<NotesDTO> notesDTO { get; set; }

        [SetUp]
        public void SetUp()
        {
            notes = GetListofNotes().AsEnumerable();
            notesDTO = GetListofNotesDTO().ToList();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mockNotesRepo = new Mock<INotesRepository>();
            _notesController = new NotesController(_unitOfWork.Object);
        }

        [Test]
        public void GetNotes_WhenCalled_ReturnOkResult()
        {
            //arrange
            _mockNotesRepo.Setup(x => x.GetAll()).Returns(notes);
            _unitOfWork.Setup(x => x.Notes).Returns(_mockNotesRepo.Object);

            //act
            IActionResult actionResult = (IActionResult)_notesController.GetNotes();
            var OkResult = actionResult as OkObjectResult;

            //assert
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(200, OkResult.StatusCode);

        }

        [Test]
        public void GetSendNotes_WhenCalled_ReturnOkResult()
        {
            //arrange
            _mockNotesRepo.Setup(x => x.SentNotes(1)).Returns(notesDTO);
            _unitOfWork.Setup(x => x.Notes).Returns(_mockNotesRepo.Object);

            //act
            IActionResult actionResult = (IActionResult)_notesController.GetSendNotes(1);
            var OkResult = actionResult as OkObjectResult;

            //assert
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(200, OkResult.StatusCode);

        }

        [Test]
        public void GetReceivedNotes_WhenCalled_ReturnOkResult()
        {
            //arrange
            _mockNotesRepo.Setup(x => x.ReceivedNotes(1)).Returns(notesDTO);
            _unitOfWork.Setup(x => x.Notes).Returns(_mockNotesRepo.Object);

            //act
            IActionResult actionResult = (IActionResult)_notesController.ReceivedNotes(1);
            var OkResult = actionResult as OkObjectResult;

            //assert
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(200, OkResult.StatusCode);

        }

        [Test]
        public void PostSentNote_WhenCalled_ReturnOkResult()
        {
            //arrange

            Notes notes = new Notes
            {                
                IsActive = true,
                Message = "msg",
                date = Convert.ToDateTime("2022-03-16"),
                ReceiverId = 8,
                SenderId = 9,
                UserId = 8,
                UrgencyLevel = "Urgent"
            };

            _mockNotesRepo.Setup(x => x.Add(notes));
            _unitOfWork.Setup(x => x.Notes).Returns(_mockNotesRepo.Object);

            //act
            IActionResult actionResult = (IActionResult)_notesController.AddNote(notes);
            var OkResult = actionResult as OkObjectResult;

            //assert
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(200, OkResult.StatusCode);

        }

        [Test]
        public void PostSentNote_WhenCalled_ReturnBadRequestResult()
        {
            //arrange

            Notes notes = null;

            _mockNotesRepo.Setup(x => x.Add(notes));
            _unitOfWork.Setup(x => x.Notes).Returns(_mockNotesRepo.Object);

            //act
            IActionResult actionResult = (IActionResult)_notesController.AddNote(notes);
            var OkResult = actionResult as BadRequestObjectResult;

            //assert
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(400, OkResult.StatusCode);

        }

        private List<Notes> GetListofNotes()
        {
            return new List<Notes>()
            {
            new Notes { Id=1, IsActive=true, Message="msg", date= Convert.ToDateTime("2022-03-16"), ReceiverId=8, SenderId=9, UserId=8, UrgencyLevel="Urgent"   }
            };
        }

        private List<NotesDTO> GetListofNotesDTO()
        {
            return new List<NotesDTO>()
            {
            new NotesDTO { Id=1, IsActive=true, Message="msg", date= Convert.ToDateTime("2022-03-16"), ReceiverId=8, SenderId=9, UserId=8, UrgencyLevel="Urgent"   }
            };
        }
    }
}
