namespace DogGo.Models.ViewModels
{
    public class SubmitWalkViewModel
    {
        public Walks Walk { get; set; }
        public List<Walker> Walkers { get; set; }
        public List<Dog> Dogs { get; set; }
        public List<Neighborhood> Neighborhoods { get; set; }
        public List<int> SelectedDogs { get; set; }
    }
}
