using PMS.Domain.Entiites;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repository
{
    public class DrugDataRepository : GenericRepository<DrugData>, IDrugDataRepository
    {
        public DrugDataRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}
