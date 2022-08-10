using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data.DAL;
using SampleWebAPI.DTO;

namespace SampleWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly IType _typeDAL;
        private readonly IMapper _mapper;
        public TypesController(IType typeDAL, IMapper mapper)
        {
            _typeDAL = typeDAL;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(TypeCreateDTO typeCreateDto)
        {
            try
            {
                var newType = _mapper.Map<SampleWebAPI.Domain.Type>(typeCreateDto);
                var result = await _typeDAL.Insert(newType);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }


  

}
