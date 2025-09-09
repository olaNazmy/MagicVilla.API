using System.ComponentModel.DataAnnotations;

namespace MagicVilla.API.Models.DTO
{
    public class VillaNumberUpdateDTO
    {
        [Required]
        public int VillNo { get; set; }
        public string SpecialDetails { get; set; }
    }
}
