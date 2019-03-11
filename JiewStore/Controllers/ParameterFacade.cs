using JiewStore.Models;
using JiewStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiewStore.Controllers
{
    public static class ParameterFacade
    {
        public static ParameterDto GetParameters()
        {
            using (JiewStoreEntities context = new JiewStoreEntities())
            {
                return GetParameters(context);
            }
        }

        public static float GetPointPerAmount(JiewStoreEntities context) {

            var parameterQry = context.Parameters.Where(p => p.Name == "PointPerAmount");

            if (parameterQry.Any())
            {
                return parameterQry.First().Value;
            }

            return 50f; //default
        }

        public static ParameterDto GetParameters(JiewStoreEntities context)
        {
            ParameterDto result = new ParameterDto
            {
                PointPerAmount = 50f
            };

            result.PointPerAmount = GetPointPerAmount(context);

            var tierQry = from t in context.CustomerTiers
                          select new TierLevelDto() {
                              ID = t.ID,
                              Name = t.Name,
                              MinAmount = t.MinAmount,
                              MaxAmount = t.MaxAmount
                          };

            if (tierQry.Any())
            {
                result.TierLevels = tierQry.ToArray();
            }

            return result;
        }
    }
}