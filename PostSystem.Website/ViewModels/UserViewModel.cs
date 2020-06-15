using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostSystem.Website.ViewModels
{
    [Serializable]
    public class UserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
