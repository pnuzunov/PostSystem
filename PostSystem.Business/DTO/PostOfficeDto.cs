using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PostSystem.Business.DTO
{
    public class PostOfficeDto : BaseDto
    {
        [Required]
        public int City_Id { get; set; }
        [Required]
        public int Office_Post_Code { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "{0} cannot exceed {1} characters. ")]
        public string Address { get; set; }
        [Required]
        public short Desk_Count { get; set; }
        public CityDto Office_City { get; set; }
    }
}
