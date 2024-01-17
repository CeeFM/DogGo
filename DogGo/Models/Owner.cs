using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Owner
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Hmmm... You should really add a name...")]
        [MaxLength(55)]
        public string Name { get; set; }
        [Required]
        [DisplayName("Neighboorhood")]
        public int NeighborhoodId { get; set; }

        [EmailAddress]
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        [MaxLength(255)]
        public string Address { get; set; }
        [Phone]
        [DisplayName("Phone Number")]
        [StringLength(55, MinimumLength = 10)]
        public string Phone { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public List<Dog> Dogs { get; set; }
    }
}
