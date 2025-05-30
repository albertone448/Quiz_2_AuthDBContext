using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Entities.Entities;

namespace DAL.Implementations
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        public IPersonDAL PersonDAL { get; set; }

        SchoolContext context;

        public UnidadDeTrabajo(IPersonDAL personDAL, SchoolContext context)
        {
            PersonDAL = personDAL;
            this.context = context;

        }
        public void Dispose()
        {
            this.context.Dispose();
        }

        public void Complete()
        {
            context.SaveChanges();
        }
    }
}
