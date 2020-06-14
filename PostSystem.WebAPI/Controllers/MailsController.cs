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
    public class MailsController : PostSystemController<MailService, MailItemDto>
    {

        public MailsController() : base() { }

        // GET: api/Mails
        [HttpGet]
        public IEnumerable<MailItemDto> GetAll([FromQuery] decimal weight)
        {
            MailService mailService = service as MailService;
            return mailService.GetAll(weight).ToList();
            
        }

    }
}
