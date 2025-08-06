using MagicVilla.API.Data;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.API.Controllers
{
    [Route("/api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<VillaDTO> GetVillas()
        {
            return Ok(VillaStore.VillasList);
        }


        // get by id that's integer 

        [HttpGet("{id:int}",Name = "GetVillaById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<VillaDTO> GetVillaById(int id)
        {
            if (id == 0)

                return BadRequest();

            else

                return Ok(VillaStore.VillasList.FirstOrDefault(v => v.Id == id));
        }


        //Add new RecordS
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villa)
        {

            //check modelState
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (VillaStore.VillasList.FirstOrDefault(v=>v.Name.ToLower() == villa.Name.ToLower()) != null )
            {
                // say it's already exist
                ModelState.AddModelError("CustomError", "already exists !");
                return BadRequest(ModelState);
            }
            if(villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);

            }

            villa.Id = VillaStore.VillasList.OrderByDescending(v=>v.Id).FirstOrDefault().Id + 1;
            VillaStore.VillasList.Add(villa);

           return CreatedAtRoute("GetVillaById", new {id=villa.Id},villa);
        }
    }
}
