using MagicVilla.API.Data;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.API.Controllers
{
    [Route("/api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        
        // get all 

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

        // delete 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.VillasList.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.VillasList.Remove(villa);
            // in case of delete 204
            return NoContent();
        }

        // update villa

        [HttpPut("{id:int}",Name = " UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO )
        {
            if(villaDTO  == null || villaDTO.Id != id )
            {
                return BadRequest();
            }
            var villa = VillaStore.VillasList.FirstOrDefault(v => v.Id == id);
            villa.Name = villaDTO.Name;
            villa.Occupancy = villaDTO.Occupancy;
            villa.Sqft = villaDTO.Sqft;

            return NoContent();

        }


        // test patch 
        [HttpPatch("{id:int}", Name = " UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDto)
        {
            if(patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.VillasList.FirstOrDefault(v => v.Id == id);
            if(villa == null)
            {
                return BadRequest();
            }
            // use apply 
            patchDto.ApplyTo(villa,ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();

        }

    }

}
