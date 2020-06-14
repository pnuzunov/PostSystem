using System;
using System.Collections.Generic;
using System.Text;

namespace PostSystem.MVCWebApp.ViewModels
{
    public class MailViewModel : BaseViewModel
    {
        public string Description { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public decimal Weight { get; set; }
    }
}
