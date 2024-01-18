using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Walks
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime Date { get; set; }
        public int Id { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        [DisplayName("Walker")]
        public int WalkerId { get; set; }
        [Required]
        [DisplayName("Dog")]
        public int DogId { get; set; }
        [DisplayName("Dog(s)")]
        public List<int> SelectedIDs { get; set; }
        public Walker Walker { get; set; }
        public Dog Dog { get; set; }

    }
}
