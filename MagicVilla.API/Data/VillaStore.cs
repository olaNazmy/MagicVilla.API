using MagicVilla.API.Models.DTO;

namespace MagicVilla.API.Data
{
    public class VillaStore
    {
       public static List<VillaDTO> VillasList = new List<VillaDTO>
            {
                new VillaDTO { Id = 1, Name = "Hg55", Occupancy = 4, Sqft = 1200 },
                new VillaDTO { Id = 2, Name = "HL666", Occupancy = 6, Sqft = 1500 },
                new VillaDTO { Id = 3, Name = "Sea Breeze", Occupancy = 2, Sqft = 800 },
                new VillaDTO { Id = 4, Name = "Mountain Retreat", Occupancy = 5, Sqft = 1400 }
            };
    }
}
