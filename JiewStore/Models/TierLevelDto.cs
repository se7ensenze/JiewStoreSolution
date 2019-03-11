using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiewStore.Models
{
    public class TierLevelDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float MinAmount { get; set; }
        public float MaxAmount { get; set; }
    }
}