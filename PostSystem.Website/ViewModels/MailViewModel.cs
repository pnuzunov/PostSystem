using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PostSystem.Website.ViewModels
{
    public class MailViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Моля въведете описание")]
        [StringLength(250, ErrorMessage = "{0}то трябва да е по-кратко от {1} символа. ")]
        [DisplayName("Описание")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Моля въведете широчина")]
        [DisplayName("Широчина")]
        public double Width { get; set; }
        [Required(ErrorMessage = "Моля въведете дължина")]
        [DisplayName("Дължина")]
        public double Length { get; set; }
        [Required(ErrorMessage = "Моля въведете височина")]
        [DisplayName("Височина")]
        public double Height { get; set; }
        [Required(ErrorMessage = "Моля въведете тегло")]
        [DisplayName("Тегло")]
        public decimal Weight { get; set; }
    }
}
