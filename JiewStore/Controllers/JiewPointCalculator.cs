using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiewStore.Controllers
{
    public class JiewPointCalculator
        : IPointCalculator
    {
        private readonly float _PointMultipler;
        private readonly float _PointPerAmount;

        public JiewPointCalculator(float ppa, float pm) {
            _PointPerAmount = ppa;
            _PointMultipler = pm;
        }

        public int CalculatePoint(float amount)
        {
            return (int)((Math.Floor(amount / _PointPerAmount)) * _PointMultipler);
        }
    }
}