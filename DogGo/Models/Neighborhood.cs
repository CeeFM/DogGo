using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Neighborhood
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(55)]
        public string Name { get; set; }
    }
}