namespace DogGo.Models.ViewModels
{
    public class WalkFormViewModel
    {
        public Walks Walk { get; set; }
        public List<Walker> Walkers { get; set; }
        public List<Dog> Dogs { get; set; }
        public List<Neighborhood> Neighborhoods { get; set; }
    }
}
