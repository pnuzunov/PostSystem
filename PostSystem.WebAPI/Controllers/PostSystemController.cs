using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using PostSystem.Business.DTO;
using PostSystem.Business.Services;

namespace PostSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PostSystemController<TService, TDto> : ControllerBase
        where TService : IService<TDto>, new()
        where TDto : BaseDto, new()
    {
        protected readonly TService service;

        public PostSystemController()
        {
            service = new TService();
        }

        [HttpGet("{id}")]
        public ActionResult<TDto> Get([FromRoute] int id)
        {
            var result = service.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TDto dto)
        {
            if (service.Create(dto))
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] TDto dto)
        {
            dto.Id = id;
            if (service.Update(dto))
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (service.Delete(id))
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
