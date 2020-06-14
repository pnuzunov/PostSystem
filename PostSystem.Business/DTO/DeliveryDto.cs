using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PostSystem.Business.DTO
{
    public class DeliveryDto : BaseDto
    {
        [Required]
        public int Mail_Id { get; set; }
        [Required]
        public int From_Office_Id { get; set; }
        [Required]
        public int To_Office_Id { get; set; }
        [StringLength(250, ErrorMessage = "{0} cannot exceed {1} characters. ")]
        public string Details { get; set; }
        public double Tax { get; set; }
        public bool Express_Delivery { get; set; }
        public MailItemDto Delivery_Mail { get; set; }
        public PostOfficeDto From_Delivery_Office { get; set; }
        public PostOfficeDto To_Delivery_Office { get; set; }
    }
}
