using AutoMapper;
using MagicVilla.API.Data;
using MagicVilla.API.Logging;
using MagicVilla.API.Models;
using MagicVilla.API.Models.DTO;
using MagicVilla.API.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;

namespace MagicVilla.API.Controllers
{
    [Route("/api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        // api response
        protected APIResponse response;
        // inject db context
        private readonly IVillaRepository villaRepository;
        // auto mapper 
        private readonly IMapper mapper;

        public VillaAPIController(IVillaRepository _villaRepository, IMapper _mapper, APIResponse _response)
        {
            villaRepository= _villaRepository;
            mapper = _mapper;
            response = _response;
        }
        // get all 

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                IEnumerable<Villa> villas = await villaRepository.GetAllAsync();
                response.Result = (mapper.Map<List<VillaDTO>>(villas));
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { e.ToString() };
            }
            return response;
        }


            // get by id that's integer 

            [HttpGet("{id:int}",Name = "GetVillaById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetVillaById(int id)
        {
            try
            {
                if (id == 0)

                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }
                var villa = await villaRepository.GetAsync(v => v.Id == id);
                if (villa == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(response);
                }
                response.Result = (mapper.Map<VillaDTO>(villa));
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { e.ToString() };
            }
            return response;
        }


        //Add new RecordS
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO createDTO)
        {
            try
            {
                if (await villaRepository.GetAsync(v => v.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    // say it's already exist
                    ModelState.AddModelError("CustomError", "already exists !");
                    return BadRequest(createDTO);
                }

                Villa villa = mapper.Map<Villa>(createDTO);

                await villaRepository.CreateAsync(villa);
                response.Result = (mapper.Map<VillaDTO>(villa));
                response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillaById", new { id = villa.Id }, response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { e.ToString() };
            }
            return response;
        }

        // delete 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villa = await villaRepository.GetAsync(v => v.Id == id);
                if (villa == null)
                {
                    return NotFound();
                }
                villaRepository.RemoveAsync(villa);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { e.ToString() };
            }
            return response;
        }

        // update villa

        [HttpPut("{id:int}",Name = " UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaDTO villaDTO )
        {
            try
            {
                if (villaDTO == null || villaDTO.Id != id)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }
                // just use automapper 
                Villa model = mapper.Map<Villa>(villaDTO);

                villaRepository.UpdateAsync(model);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;

                return NoContent();
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { e.ToString() };
            }
            return response;

        }


        // test patch 
        [HttpPatch("{id:int}", Name = " UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            // no tracking for this model 
            var villa = await villaRepository.GetAsync(v => v.Id == id);

            // mapping 
            VillaUpdateDTO villaDTO = mapper.Map<VillaUpdateDTO>(villa);

            patchDto.ApplyTo(villaDTO, ModelState);
            Villa model = mapper.Map<Villa>(villaDTO);

            villaRepository.UpdateAsync(model);
            await villaRepository.SaveAsync();
            //
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();

        }

    }

}
