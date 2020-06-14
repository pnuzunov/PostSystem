using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PostSystem.Models
{
    public class City : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string City_Name { get; set; }
        [Required]
        public int City_Post_Code { get; set; }
        public bool HasExpressDelivery { get; set; }
        public virtual ICollection<PostOffice> PostOffices { get; set; }
    }
}
