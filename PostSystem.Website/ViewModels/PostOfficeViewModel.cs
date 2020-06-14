using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PostSystem.Website.ViewModels
{
    public class PostOfficeViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Моля въведете град")]
        [DisplayName("Град")]
        public int City_Id { get; set; }
        [Required(ErrorMessage = "Моля въведете код")]
        [Range(1000, 9999, ErrorMessage = "Невалиден пощенски код")]
        [DisplayName("Код на пощенски офис")]
        public int Office_Post_Code { get; set; }
        [StringLength(250, ErrorMessage = "{0}ът трябва да е по-кратък от {1} символа. ")]
        [Required(ErrorMessage = "Моля въведете адрес")]
        [DisplayName("Адрес")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Моля въведете брой гишета")]
        [DisplayName("Брой гишета")]
        public short Desk_Count { get; set; }
        public CityViewModel Office_City { get; set; }
    }
}
