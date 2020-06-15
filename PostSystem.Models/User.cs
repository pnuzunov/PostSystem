using System;
using System.Collections.Generic;
using System.Text;

namespace PostSystem.Models
{
    class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int AuthMask { get; set; }
    }
}
