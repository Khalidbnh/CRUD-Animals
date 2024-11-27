using WebApplication9.DAL;

namespace WebApplication9.Models.ViewModel
{
    public class AnimalViewModel
    {
        public List<AnimalModel> Animales { get; set; } = new List<AnimalModel>();
        public List<TipoAnimal> TipoAnimals { get; set; } = new List<TipoAnimal>();


        public AnimalViewModel()
        {
            
        }
    }
}
