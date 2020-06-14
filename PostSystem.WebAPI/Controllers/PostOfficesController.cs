using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostSystem.Business.DTO;
using PostSystem.Business.Services;

namespace PostSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostOfficesController : PostSystemController<PostOfficeService, PostOfficeDto>
    {
        public PostOfficesController() : base() { }

        [HttpGet]
        public IEnumerable<PostOfficeDto> GetAll([FromQuery] string city)
        {
            PostOfficeService postOfficeService = service as PostOfficeService;
            return postOfficeService.GetAll(city).ToList();

        }
    }
}
