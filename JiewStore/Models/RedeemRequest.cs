using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiewStore.Models
{
    public class RedeemRequest
    {
        public string Code { get; set; }
        public int Amount { get; set; }
    }
}