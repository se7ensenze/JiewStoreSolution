using JiewStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiewStore.Controllers
{
    public static class TransactionHelper
    {
        public static Transaction MakeNewAmountTransaction(Customer owner, float amount, float ppa, float pm, string remark, IPointCalculator calculator)
        {
            if (amount <= 0)
            {
                throw new Exception("amount is less than or equal zero");
            }

            if (owner == null)
            {
                throw new Exception("owner is null");
            }

            float pointPerAmount = ppa;
            float pointMultipler = pm;

            int transactionPoint = calculator.CalculatePoint(amount);

            var newTransaction = new Transaction()
            {
                Amount = amount,
                Point = transactionPoint,
                PointPerAmount = pointPerAmount,
                PointMultiplier = pointMultipler,
                Customer = owner, //attach to new customer
                Remark = remark ?? "",
                RecordTime = DateTime.Now
            };

            return newTransaction;
        }

        public static Transaction MakeNewAddPointTransaction(Customer owner, int point)
        {
            if (point <= 0)
            {
                throw new Exception("point is less than or equal zero");
            }

            if (owner == null)
            {
                throw new Exception("owner is null");
            }

            var newTransaction = new Transaction()
            {
                Amount = 0,
                Point = point,
                PointPerAmount = 0,
                PointMultiplier = 0,
                Customer = owner, //attach to new customer
                Remark = "add point",
                RecordTime = DateTime.Now
            };

            return newTransaction;
        }

        public static Transaction MakeNewRedeemPointTransaction(Customer owner, int point)
        {
            if (point <= 0)
            {
                throw new Exception("point is less than or equal zero");
            }

            if (owner == null)
            {
                throw new Exception("owner is null");
            }

            var newTransaction = new Transaction()
            {
                Amount = 0,
                Point = -point,
                PointPerAmount = 0,
                PointMultiplier = 0,
                Customer = owner, //attach to new customer
                Remark = "redeem",
                RecordTime = DateTime.Now
            };

            return newTransaction;
        }

    }

}
