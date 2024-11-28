using System.ComponentModel.DataAnnotations;

namespace WebApplication9.Models.ViewModel
{
    public class LoginViewModel
    {

        [Required(ErrorMessage ="El nombre del Usuario es obligatoriooooooooo")]
        [Display(Name ="Nombre del Usuariooooo")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El Contraseña es obligatoriaaaaa")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseñaaaaaaaaa")]
        public string Password { get; set; }
    }
}
