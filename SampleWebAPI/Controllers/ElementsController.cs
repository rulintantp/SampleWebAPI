using AutoMapper;
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
    public class ElementController : ControllerBase
    {
        private readonly IElement _elementDAL;
        private readonly IMapper _mapper;
       public ElementController(IElement elementDAL, IMapper mapper)
        {
            _elementDAL = elementDAL;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult> Post(ElementCreateDTO elementCreateDto)
        {
            try
            {
                var newElement = _mapper.Map<Element>(elementCreateDto);
                var result = await _elementDAL.Insert(newElement);
                var elementReadDto = _mapper.Map<ElementReadDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, elementReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ElementReadDTO> Get(int id)
        {
            try
            {
                var result = await _elementDAL.GetById(id);
                if (result == null)
                {
                    throw new Exception($"data {id} tidak ditemukan");
                }
                var elementDTO = _mapper.Map<ElementReadDTO>(result);

                return elementDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<ElementReadDTO>> Get()
        {
            var results = await _elementDAL.GetAll();
            var elementDTO = _mapper.Map<IEnumerable<ElementReadDTO>>(results);

            return elementDTO;
        }

        [HttpPut]
        public async Task<ActionResult> Put(ElementReadDTO elementDto)
        {
            try
            {
                var updateElement = new Element
                {
                    Id = elementDto.Id,
                    Name = elementDto.Name
                };
                var result = await _elementDAL.Update(updateElement);
                return Ok(elementDto);
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
                await _elementDAL.Delete(id);
                return Ok($"Data sword dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
