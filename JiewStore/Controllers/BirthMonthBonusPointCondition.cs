using JiewStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiewStore.Controllers
{
    public static class BirthMonthBonusPointCondition
    {
        public static float GetMultipler(DateTime birthDate)
        {
            if (birthDate.Month == DateTime.Now.Month)
            {
                return 2f;
            }
            else {
                return 1f;
            }
        }
    }
}
