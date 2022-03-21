using PMS.Domain.DTO;
using PMS.Domain.Entiites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Interfaces
{
    public interface INotesRepository : IGenericRepository<Notes>
    {
        List<NotesDTO> SentNotes(int userId);
        List<NotesDTO> ReceivedNotes(int userId);
    }
}
