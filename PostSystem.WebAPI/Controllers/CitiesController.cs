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
    public class CitiesController : PostSystemController<CityService, CityDto>
    {
        public CitiesController() : base() { }

        public IEnumerable<CityDto> GetAll([FromQuery] int postCode)
        {
            CityService cityService = service as CityService;
            return cityService.GetAll(postCode).ToList();
        }
    }
}
