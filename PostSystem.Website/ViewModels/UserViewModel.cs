using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostSystem.Website.ViewModels
{
    [Serializable]
    public class UserViewModel
    {
        [Required(ErrorMessage = "Моля въведете потребителско име")]
        [DisplayName("Потребителско име")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Моля въведете парола")]
        [DisplayName("Парола")]
        public string Password { get; set; }
    }
}
