using AutoMapper;
using MagicVilla.API.Data;
using MagicVilla.API.Logging;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MagicVilla.API.Controllers
{
    [Route("/api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {

        // inject db context
        private readonly ApplicationDbContext db;
        // auto mapper 
        private readonly IMapper mapper;

        public VillaAPIController(ApplicationDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }
        // get all 

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villas = await db.Villas.ToListAsync();
            return Ok(mapper.Map<List<VillaDTO>>(villas));
        }


        // get by id that's integer 

        [HttpGet("{id:int}",Name = "GetVillaById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<VillaDTO>> GetVillaById(int id)
        {
            if (id == 0)

            {
                return BadRequest();
            }
            var villa = await db.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<VillaDTO>(villa));
        }


        //Add new RecordS
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  async Task<ActionResult<VillaCreateDTO>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {

            if (await db.Villas.FirstOrDefaultAsync(v=>v.Name.ToLower() == createDTO.Name.ToLower()) != null )
            {
                // say it's already exist
                ModelState.AddModelError("CustomError", "already exists !");
                return BadRequest(createDTO);
            }

            Villa model = mapper.Map<Villa>(createDTO);

           await  db.Villas.AddAsync(model);
           await db.SaveChangesAsync();

           return CreatedAtRoute("GetVillaById", new {id=model.Id},model);
        }

        // delete 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await db.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
             db.Villas.Remove(villa);
            await db.SaveChangesAsync();
            return NoContent();
        }

        // update villa

        [HttpPut("{id:int}",Name = " UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaDTO villaDTO )
        {
            if(villaDTO  == null || villaDTO.Id != id )
            {
                return BadRequest();
            }
            // just use automapper 
            Villa model = mapper.Map<Villa>(villaDTO);

            db.Villas.Update(model);
            await db.SaveChangesAsync();
            return NoContent();

        }


        // test patch 
        [HttpPatch("{id:int}", Name = " UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDto)
        {
            if(patchDto == null || id == 0)
            {
                return BadRequest();
            }
            // no tracking for this model 
            var villa = await db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            // mapping 
            VillaUpdateDTO villaDTO = mapper.Map<VillaUpdateDTO>(villa);

            patchDto.ApplyTo(villaDTO, ModelState);
            Villa model = mapper.Map<Villa>(villaDTO);

            db.Villas.Update(model);
            await db.SaveChangesAsync();
            //
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();

        }

    }

}
