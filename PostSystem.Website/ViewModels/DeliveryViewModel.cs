using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PostSystem.Website.ViewModels
{
    public class DeliveryViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Моля въведете описание на колет")]
        [DisplayName("Колет")]
        public int Mail_Id { get; set; }
        [Required(ErrorMessage = "Моля въведете пощенски офис")]
        [DisplayName("От пощенски офис")]
        public int From_Office_Id { get; set; }
        [Required(ErrorMessage = "Моля въведете пощенски офис")]
        [DisplayName("Към пощенски офис")]
        public int To_Office_Id { get; set; }
        [Required(ErrorMessage = "Моля въведете описание")]
        [StringLength(250, ErrorMessage = "Полето за описанието трябва да е по-кратко от {1} символа. ")]
        [DisplayName("Описание")]
        public string Details { get; set; }
        [DisplayName("Такса")]
        public double Tax { get; set; }
        [DisplayName("Експресна услуга")]
        public bool Express_Delivery { get; set; }
        public MailViewModel Delivery_Mail { get; set; }
        public PostOfficeViewModel From_Delivery_Office { get; set; }
        public PostOfficeViewModel To_Delivery_Office { get; set; }
    }
}
