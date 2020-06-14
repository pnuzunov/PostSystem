using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PostSystem.Business.DTO
{
    public class CityDto : BaseDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "{0} cannot exceed {1} characters. ")]
        public string City_Name { get; set; }
        [Required]
        public int City_Post_Code { get; set; }
        public bool HasExpressDelivery { get; set; }
    }
}
