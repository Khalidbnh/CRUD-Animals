using System.ComponentModel.DataAnnotations;

namespace WebApplication9.Models.ViewModel
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "El nombre del Usuario es obligatorioooo")]
        [StringLength(10, ErrorMessage = "El Nombre del usuario no puede tener más de 20 caracteres")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoriaaaa")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 6,ErrorMessage ="La contraseña debe tener al menos de 6 caracteres")]

        public string Password { get; set; }

        [Required(ErrorMessage = "Debe confirmar la contraseñaaaa")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "No iguales")]
        public string ConfirmPassword { get; set; }

    }
}
