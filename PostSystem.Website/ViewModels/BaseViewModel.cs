using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PostSystem.Website.ViewModels
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        [DisplayName("Дата на регистриране")]
        public DateTime Created_On { get; set; }
        [DisplayName("Последна промяна")]
        public DateTime Updated_On { get; set; }
    }
}
