using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Entities.Entities;

namespace DAL.Implementations
{
    public class PersonDALImpl : GenericDALImpl<Person>, IPersonDAL
    {
        SchoolContext _context;

        public PersonDALImpl(SchoolContext context)
            : base(context)
        {
            _context = context;
        }
    }
}