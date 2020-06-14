using System;
using System.Collections.Generic;
using System.Text;

namespace PostSystem.MVCWebApp.ViewModels
{
    public class PostOfficeViewModel : BaseViewModel
    {
        public int City_Id { get; set; }
        public string Address { get; set; }
        public short Desk_Count { get; set; }
        public CityViewModel Office_City { get; set; }
    }
}
