using PMS.Domain.DTO;
using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repository
{
    public class NotesRepository : GenericRepository<Notes>, INotesRepository
    {
        public NotesRepository(ApplicationDBContext context) : base(context)
        {

        }

        public List<NotesDTO> ReceivedNotes(int userId)
        {
            List<NotesDTO> notes = new List<NotesDTO>();

            var sendnote = _context.Notes.Where(d => d.ReceiverId == userId).ToList();
            foreach (var item in sendnote)
            {
                var sender = _context.Registration.Where(d => d.UserId == item.SenderId).FirstOrDefault();
                var receiver = _context.Registration.Where(d => d.UserId == item.ReceiverId).FirstOrDefault();
                NotesDTO note = new NotesDTO();
                note.UserId = item.UserId;
                note.SenderId = item.SenderId;
                note.Sender = sender.FirstName + " " + sender.LastName;
                note.ReceiverId = item.ReceiverId;
                note.Receiver = receiver.FirstName + " " + receiver.LastName;
                note.Message = item.Message;
                note.UrgencyLevel = item.UrgencyLevel;
                note.date = item.date;
                notes.Add(note);
            }

            return notes;
        }

        public List<NotesDTO> SentNotes(int userId)
        {
            List<NotesDTO> notes = new List<NotesDTO>();

            var sendnote = _context.Notes.Where(d => d.SenderId == userId).ToList();
            foreach (var item in sendnote)
            {
                var sender = _context.Registration.Where(d => d.UserId == item.SenderId).FirstOrDefault();
                var receiver = _context.Registration.Where(d => d.UserId == item.ReceiverId).FirstOrDefault();
                NotesDTO note = new NotesDTO();
                note.UserId = item.UserId;
                note.SenderId = item.SenderId;
                note.Sender = sender.FirstName + " " + sender.LastName;                
                note.ReceiverId = item.ReceiverId;
                note.Receiver = receiver.FirstName + " " + receiver.LastName;
                note.Message = item.Message;
                note.UrgencyLevel = item.UrgencyLevel;
                note.date = item.date;
                notes.Add(note);
            }

            return notes;
        }
    }
}
