using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data.DAL;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;
using SampleWebAPI.Helpers;

namespace SampleWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SwordsController : ControllerBase
    {
        private readonly ISword _swordDAL;
        private readonly IMapper _mapper;
        public SwordsController(ISword swordDAL, IMapper mapper)
        {
            _swordDAL = swordDAL;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(SwordCreateDTO swordCreateDto)
        {
            try
            {
                var newSword = _mapper.Map<Sword>(swordCreateDto);
                var result = await _swordDAL.Insert(newSword);
                var swordReadDto = _mapper.Map<SwordReadDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, swordReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<SwordReadDTO> Get(int id)
        {
            try
            {
                var result = await _swordDAL.GetById(id);
                if (result == null)
                {
                    throw new Exception($"data {id} tidak ditemukan");
                }
                var swordDTO = _mapper.Map<SwordReadDTO>(result);

                return swordDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<SwordReadDTO>> Get()
        {
            var results = await _swordDAL.GetAll();
            var swordDTO = _mapper.Map<IEnumerable<SwordReadDTO>>(results);

            return swordDTO;
        }

        [HttpPut]
        public async Task<ActionResult> Put(SwordReadDTO swordDto)
        {
            try
            {
                var updateSword = new Sword
                {
                    Id = swordDto.Id,
                    Name = swordDto.Name
                };
                var result = await _swordDAL.Update(updateSword);
                return Ok(swordDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _swordDAL.Delete(id);
                return Ok($"Data sword dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddElementSword")]
        public async Task<ActionResult> AddElementSword(ElementSwordDTO elementSwordDto)
        {
            try
            {
                var newElementSword = _mapper.Map<ElementSword>(elementSwordDto);
                var result = await _swordDAL.AddElementSword(newElementSword);


                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddSwordWithType")]
        public async Task<ActionResult> AddSwordWithType(SwordWithTypeDTO swordWithTypeDTO)
        {
            try
            {
                var newSword = new Sword();
                newSword.Name = swordWithTypeDTO.Name;
                newSword.Weight = swordWithTypeDTO.Weight;
                newSword.SamuraiId = swordWithTypeDTO.SamuraiId;

                var newType = new SampleWebAPI.Domain.Type();
                newType.Name = swordWithTypeDTO.Types.Name;
                newSword.Type = newType;

                var result = await _swordDAL.Insert(newSword);

                return Ok(newSword);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteElementFromSword")]
        public async Task<ActionResult> DeleteElementFromSword(int id)
        {
            try
            {
                await _swordDAL.DeleteElementFromSword(id);
                return Ok($"Data sword dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("WithType")]
        public async Task <IActionResult> GetSwordWithType(int pageNumber)
        {
            var results = await _swordDAL.GetSwordWithType();
             var swordTypeDTO = _mapper.Map<IEnumerable<SwordTypeDTO>>(results);
            var pagging = swordTypeDTO.Skip((pageNumber - 1) * 10).Take(10).ToList();
            return Ok(pagging) ;
        }

    }
}
