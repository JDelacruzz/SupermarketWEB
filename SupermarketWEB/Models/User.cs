using System.ComponentModel.DataAnnotations;

namespace Autenticacion.Models
{
    public class User
    {
        [Required] // Verificar que se importó using System.ComponentModel.DataAnnotations;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
