using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PostSystem.Models
{
    public class Delivery : BaseEntity
    {
        [Required]
        public int Mail_Id { get; set; }
        [Required]
        public int From_Office_Id { get; set; }
        [Required]
        public int To_Office_Id { get; set; }
        [StringLength(250)]
        public string Details { get; set; }
        public double Tax { get; set; }
        public bool Express_Delivery { get; set; }
        public virtual MailItem Delivery_Mail { get; set; }
        public virtual PostOffice From_Delivery_Office { get; set; }
        public virtual PostOffice To_Delivery_Office { get; set; }
    }
}
