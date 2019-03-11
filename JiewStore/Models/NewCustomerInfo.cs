using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiewStore.Models
{
    public class NewCustomerInfo
    {
        public string NickName { get; set; }
        public string FullName { get; set; }
        public string Facebook { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public float Amount { get; set; }
    }
}