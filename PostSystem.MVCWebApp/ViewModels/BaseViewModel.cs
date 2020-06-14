using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostSystem.MVCWebApp.ViewModels
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public DateTime Created_On { get; set; }
        public DateTime Updated_On { get; set; }
    }
}
