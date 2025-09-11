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
    [Route("/api/VillaNumberAPI")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        // api response
        protected APIResponse response;
        // inject db context
        private readonly IVillaNumberRepository villaNumberRepository;
        // auto mapper 
        private readonly IMapper mapper;

        public VillaNumberAPIController(IVillaNumberRepository _villaNumberRepository, IMapper _mapper, APIResponse _response)
        {
            villaNumberRepository= _villaNumberRepository;
            mapper = _mapper;
            response = _response;
        }
        // get all 

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumberList = await villaNumberRepository.GetAllAsync();
                response.Result = (mapper.Map<List<VillaNumberDTO>>(villaNumberList));
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
             
            [HttpGet("{id:int}",Name = "GetVillaNumberById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetVillaNumberById(int id)
        {
            try
            {
                if (id == 0)

                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }
                var villaNumber = await villaNumberRepository.GetAsync(v => v.VillNo == id);
                if (villaNumber == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(response);
                }
                response.Result = (mapper.Map<VillaNumberDTO>(villaNumber));
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
        public  async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
        {
            try
            {
                if (await villaNumberRepository.GetAsync(v => v.VillNo == createDTO.VillNo) != null)

                {
                    // say it's already exist
                    ModelState.AddModelError("CustomError", "already exists !");
                    return BadRequest(createDTO);
                }

                VillaNumber villaNumber = mapper.Map<VillaNumber>(createDTO);

                await villaNumberRepository.CreateAsync(villaNumber);
                response.Result = (mapper.Map<VillaNumberDTO>(villaNumber));
                response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillaNumberById", new { id = villaNumber.VillNo }, response);
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
        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var villaNumber = await villaNumberRepository.GetAsync(v => v.VillNo == id);
                if (villaNumber == null)
                {
                    return NotFound();
                }
                villaNumberRepository.RemoveAsync(villaNumber);
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

        [HttpPut("{id:int}",Name = " UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberDTO villaNumberDTO )
        {
            try
            {
                if (villaNumberDTO == null || villaNumberDTO.VillNo != id)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }
                // just use automapper 
                VillaNumber villaNumberModel = mapper.Map<VillaNumber>(villaNumberDTO);

                villaNumberRepository.UpdateAsync(villaNumberModel);
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
        [HttpPatch("{id:int}", Name = " UpdatePartialVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVillaNumber(int id, JsonPatchDocument<VillaNumberUpdateDTO> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            // no tracking for this model 
            var villaNumber = await villaNumberRepository.GetAsync(v => v.VillNo == id);

            // mapping 
            VillaNumberUpdateDTO villaNumberDTO = mapper.Map<VillaNumberUpdateDTO>(villaNumber);

            patchDto.ApplyTo(villaNumberDTO, ModelState);
            VillaNumber villaNumberModel = mapper.Map<VillaNumber>(villaNumberDTO);

            villaNumberRepository.UpdateAsync(villaNumberModel);
            await villaNumberRepository.SaveAsync();
            //
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();

        }

    }

}
