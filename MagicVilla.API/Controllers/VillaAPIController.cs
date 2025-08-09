using MagicVilla.API.Data;
using MagicVilla.API.Logging;
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

        // inject db context
        private readonly ApplicationDbContext db;
        public VillaAPIController(ApplicationDbContext _db)
        {
            db = _db;

        }
        // get all 

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<VillaDTO> GetVillas()
        {
            return Ok(db.Villas.ToList());
        }


        // get by id that's integer 

        [HttpGet("{id:int}",Name = "GetVillaById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<VillaDTO> GetVillaById(int id)
        {
            if (id == 0)

            {
                return BadRequest();
            }

            else

                return Ok(db.Villas.FirstOrDefault(v => v.Id == id));
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

            if (db.Villas.FirstOrDefault(v=>v.Name.ToLower() == villa.Name.ToLower()) != null )
            {
                // say it's already exist
                ModelState.AddModelError("CustomError", "already exists !");
                return BadRequest(ModelState);
            }
            if(villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);

            }
            // convert from dto to villa ,, use auto mapping later 
            Villa model = new Villa()
            {
                Id = villa.Id,
                Name = villa.Name,
                Amenity = villa.Amenity,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy,
                CreationDate = villa.CreationDate,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
                UpdatedDate = villa.UpdatedDate
               
            };

            db.Villas.Add(model);
            db.SaveChanges();

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
            var villa = db.Villas.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            db.Villas.Remove(villa);
            db.SaveChanges();
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
            //var villa = db.Villas.FirstOrDefault(v => v.Id == id);
            //villa.Name = villaDTO.Name;
            //villa.Occupancy = villaDTO.Occupancy;
            //villa.Sqft = villaDTO.Sqft;

            // convert dto to villa model
            Villa model = new Villa()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                CreationDate = villaDTO.CreationDate,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                UpdatedDate = villaDTO.UpdatedDate

            };
            db.Villas.Update(model);
            db.SaveChanges();
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
            var villa = db.Villas.FirstOrDefault(v => v.Id == id);

            VillaDTO villaDTO = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Amenity = villa.Amenity,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy,
                CreationDate = villa.CreationDate,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
                UpdatedDate = villa.UpdatedDate
            };

            patchDto.ApplyTo(villaDTO, ModelState);

            Villa model = new Villa()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                CreationDate = villaDTO.CreationDate,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                UpdatedDate = villaDTO.UpdatedDate

            };

            db.Villas.Update(model);
            db.SaveChanges();
            //
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();

        }

    }

}
