using System.ComponentModel.DataAnnotations;

namespace MagicVilla.API.Models.DTO
{
    public class VillaCreateDTO
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
