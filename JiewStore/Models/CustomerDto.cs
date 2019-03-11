using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiewStore.Models
{
    public class CustomerDto
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Facebook { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public System.DateTime BirthDate { get; set; }
        public int RemainingPoint { get; set; }
        public string TierName { get; set; }
    }
}