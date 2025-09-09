using System.ComponentModel.DataAnnotations;

namespace MagicVilla.API.Models.DTO
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int VillNo { get; set; }
        public string SpecialDetails { get; set; }
    }
}
