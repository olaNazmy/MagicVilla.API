using System.ComponentModel.DataAnnotations;

namespace MagicVilla.API.Models.DTO
{
    public class VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public int Sqft { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
