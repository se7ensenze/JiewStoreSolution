using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiewStore.Models
{
    public class CustomerInfoRequest
    {
        public string Code { get; set; }
        public bool IncludeRemainingPoint { get; set; }
    }
}