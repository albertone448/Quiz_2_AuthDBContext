using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Entities.Entities;

namespace DAL.Implementations
{
    public class DepartmentDALImpl : GenericDALImpl<Department>, IDepartmentDAL
    {
        SchoolContext _context;
        public DepartmentDALImpl(SchoolContext context)
            : base(context)
        {
            _context = context;
        }


    }
}
