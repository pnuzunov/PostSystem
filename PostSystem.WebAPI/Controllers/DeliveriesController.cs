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
    public class DeliveriesController : PostSystemController<DeliveryService, DeliveryDto>
    {
        public DeliveriesController() : base() { }

        /*
        public IEnumerable<DeliveryDto> GetAll([FromQuery] int fromOfficeId)
        {
            DeliveryService deliveryService = service as DeliveryService;
            return deliveryService.GetAll(fromOfficeId).ToList();
        }
        */

        public IEnumerable<DeliveryDto> GetAll([FromQuery] string details)
        {
            DeliveryService deliveryService = service as DeliveryService;
            return deliveryService.GetAll(details).ToList();
        }
    }
}
