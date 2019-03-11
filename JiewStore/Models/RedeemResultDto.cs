using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiewStore.Models
{
    public class RedeemResultDto
    {
        public RedeemResultDto() {
            RecordTime = DateTime.Now;
        }
        public string Code { get; set; }
        public int Point { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTime RecordTime { get; set; }
    }
}