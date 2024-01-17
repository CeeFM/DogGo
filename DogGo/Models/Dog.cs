using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(55)]
        public string Name { get; set; }
        [Required]
        [MaxLength(55)]
        public string Breed { get; set; }
        [Required]
        [DisplayName("Owner")]
        public int OwnerId { get; set; }
        [MaxLength(255)]
        public string? Notes { get; set; }
        [DisplayName("Dog Photo (URL)")]
        [MaxLength(255)]
        public string? ImageUrl { get; set; }
        public Owner Owner { get; set; }

    }
}
