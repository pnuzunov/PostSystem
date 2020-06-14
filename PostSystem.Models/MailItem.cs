using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PostSystem.Models
{
    public class MailItem : BaseEntity
    {
        [StringLength(250)]
        public string Description { get; set; }
        [Required]
        public double Width { get; set; }
        [Required]
        public double Length { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public decimal Weight { get; set; }
        public virtual Delivery Delivery { get; set; } 
    }
}
