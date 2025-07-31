using MagicVilla.API.Data;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.API.Controllers
{
    [Route("/api/VillaAPI")]
    [ApiController]
    public class VillaAPIController :ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas()
        {
            return VillaStore.VillasList;
        }


        // get by id that's integer 
        [HttpGet("{id:int}")]
        public VillaDTO GetVillaById(int id)
        {
            return VillaStore.VillasList.FirstOrDefault(v=>v.Id == id);
        }
    }
}
