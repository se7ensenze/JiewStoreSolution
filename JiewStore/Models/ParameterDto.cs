using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiewStore.Models
{
    public class ParameterDto
    {
        public TierLevelDto[] TierLevels { get; set; }
        public float PointPerAmount { get; set; }
    }
}