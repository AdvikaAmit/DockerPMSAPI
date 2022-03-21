using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repository
{
    public class PatientDetailsRepository : GenericRepository<PatientDetails>, IPatientDetailsRepository
    {
        public PatientDetailsRepository(ApplicationDBContext context) : base(context)
        {

        }

        public PatientDetails GetPatientDemographicDetails(int id)
        {
            PatientDetails patientdata = new PatientDetails();
            
            var data = _context.PatientDetails.Where(d => d.UserId == id).FirstOrDefault();
            if(data == null)
            {
                var reguser = _context.Registration.Where(d => d.UserId == id).FirstOrDefault();

                patientdata.UserId = reguser.UserId;
                patientdata.Title = reguser.Title;
                patientdata.FirstName = reguser.FirstName;
                patientdata.LastName = reguser.LastName;
                patientdata.EmailId = reguser.EmailId;
                patientdata.ContactNo = reguser.ContactNo;
                patientdata.DOB = reguser.DOB;
            }
            else
            {
                patientdata = data;
            }
            return patientdata;
        }
    }
}
