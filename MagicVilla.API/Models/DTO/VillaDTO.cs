using System.ComponentModel.DataAnnotations;

namespace MagicVilla.API.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;



    }
}
