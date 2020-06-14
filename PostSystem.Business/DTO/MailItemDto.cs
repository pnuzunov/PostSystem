using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PostSystem.Business.DTO
{
    public class MailItemDto : BaseDto
    {
        [Required]
        [StringLength(250, ErrorMessage = "{0} cannot exceed {1} characters. ")]
        public string Description { get; set; }
        [Required]
        public double Width { get; set; }
        [Required]
        public double Length { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public decimal Weight { get; set; }
    }
}
