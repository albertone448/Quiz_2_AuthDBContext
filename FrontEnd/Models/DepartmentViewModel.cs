using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class DepartmentViewModel
    {
        [Display(Name = "Identificador")]
        public int DepartmentId { get; set; }

        [Display(Name = "Nombre")]
        public string Name { get; set; } = null!;

        [Display(Name = "Presupuesto")]
        public decimal Budget { get; set; }


        [Display(Name = "Fecha de inicio")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Administrador")]
        public int? Administrator { get; set; }
    }
}
