using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PostSystem.Website.ViewModels
{
    public class CityViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Моля въведете име")]
        [StringLength(50, ErrorMessage = "{0}то трябва да е по-кратко от {1} символа. ")]
        [DisplayName("Име")]
        public string City_Name { get; set; }
        [Required(ErrorMessage = "Моля въведете пощенски код")]
        [DisplayName("Пощенски код")]
        public int City_Post_Code { get; set; }
        public bool HasExpressDelivery { get; set; }
    }
}
