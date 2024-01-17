using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Walker
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(55)]
        public string Name { get; set; }
        [Required]
        [DisplayName("Neighborhood")]
        public int NeighborhoodId { get; set; }
        [DisplayName("Walker Photo (URL)")]
        [MaxLength(255)]
        public string ImageUrl { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}