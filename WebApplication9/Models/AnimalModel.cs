using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace WebApplication9.Models
{
    public class AnimalModel
    {
        public int IdAnimal { get; set; }

        [Required(ErrorMessage = "Nombre del Animal es obligatorio.")]
        public string NombreAnimal { get; set; }

        public string Raza { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de animal.")]
        public int RIdTipoAnimal { get; set; }

        public DateTime? FechaNacimiento { get; set; }
        
        public TipoAnimal TipoAnimal { get; set; }

    }
}
