using System;
using System.Collections.Generic;
using System.Text;

namespace PostSystem.Business.DTO
{
    public class BaseDto
    {
        public int Id { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime Updated_On { get; set; }
    }
}
