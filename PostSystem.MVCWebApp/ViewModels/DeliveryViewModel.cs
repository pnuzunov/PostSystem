using System;
using System.Collections.Generic;
using System.Text;

namespace PostSystem.MVCWebApp.ViewModels
{
    public class DeliveryViewModel : BaseViewModel
    {
        public int Mail_Id { get; set; }
        public int From_Office_Id { get; set; }
        public int To_Office_Id { get; set; }
        public string Details { get; set; }
        public double Tax { get; set; }
        public bool Express_Delivery { get; set; }
        public MailViewModel Delivery_Mail { get; set; }
        public PostOfficeViewModel From_Delivery_Office { get; set; }
        public PostOfficeViewModel To_Delivery_Office { get; set; }
    }
}
