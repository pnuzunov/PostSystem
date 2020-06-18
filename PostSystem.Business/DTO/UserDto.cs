using System;
using System.Collections.Generic;
using System.Text;

namespace PostSystem.Business.DTO
{
    public class UserDto : BaseDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
