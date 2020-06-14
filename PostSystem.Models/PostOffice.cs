using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PostSystem.Models
{
    public class PostOffice : BaseEntity
    {
        [Required]
        public int City_Id { get; set; }
        [Required]
        public int Office_Post_Code { get; set; }
        [Required]
        [StringLength(250)]
        public string Address { get; set; }
        [Required]
        public short Desk_Count { get; set; }
        public virtual City Office_City { get; set; }
        public virtual ICollection<Delivery> From_Deliveries { get; set; }
        public virtual ICollection<Delivery> To_Deliveries { get; set; }
    }
}
