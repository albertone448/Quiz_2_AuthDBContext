using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class PersonViewModel
    {
        [Display(Name = "ID de Persona")]
        public int PersonID { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder 50 caracteres")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Fecha de Contratación")]
        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }

        [Display(Name = "Fecha de Inscripción")]
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }

        [Display(Name = "Tipo de Persona")]
        [Required(ErrorMessage = "El discriminador es requerido")]
        [StringLength(50, ErrorMessage = "El discriminador no puede exceder 50 caracteres")]
        public string Discriminator { get; set; } = null!;
    }
}