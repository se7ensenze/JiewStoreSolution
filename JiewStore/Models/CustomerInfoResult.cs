using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiewStore.Models
{
    public class CustomerInfoResult
    {
        public CustomerInfoResult()
        {
            RecordTime = DateTime.Now;
        }
        
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public CustomerDto Customer { get; set; }
        public DateTime RecordTime { get; set; }
    }
}