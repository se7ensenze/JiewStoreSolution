using JiewStore.DAL;
using JiewStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace JiewStore.Controllers
{
    public static class CustomerFacade
    {
        public static CustomerDto NewCustomer(string nickName, string fullName, string facebook, 
            string phone, DateTime birthDate, string address, float firstTimeAmount)
        {
            using (JiewStoreEntities context = new JiewStoreEntities())
            {
                float pointPerAmount = ParameterFacade.GetPointPerAmount(context);
                float pointMultipler = BirthMonthBonusPointCondition.GetMultipler(birthDate); ;

                IPointCalculator pointCalculator = new JiewPointCalculator(pointPerAmount, pointMultipler);

                Customer newCustomer = CustomerHelper.NewCustomer(nickName,
                    fullName,
                    facebook,
                    phone,
                    birthDate,
                    address);

                Transaction newTransaction = TransactionHelper.MakeNewAmountTransaction(newCustomer,
                    firstTimeAmount,
                    pointPerAmount,
                    pointMultipler,
                    "new customer",
                    pointCalculator);

                try
                {
                    context.Customers.Add(newCustomer);
                    context.Transactions.Add(newTransaction);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

                return AutoMapper.Mapper.Map<CustomerDto>(newCustomer);

            } //using (JiewStoreEntities context = new JiewStoreEntities())
        }

        public static CustomerInfoResult GetCustomer(string code, bool includeRemainingAmount)
        {
            using (JiewStoreEntities context = new JiewStoreEntities())
            {
                try
                {
                    var customerQry = from c in context.Customers
                                      join t in context.CustomerTiers
                                      on c.CustomerTierID equals t.ID
                                      select new CustomerDto
                                      {
                                          ID = c.ID,
                                          Code = c.Code,
                                          NickName = c.NickName,
                                          FullName = c.FullName,
                                          Facebook = c.Facebook,
                                          Phone = c.Phone,
                                          Address = c.Address,
                                          BirthDate = c.BirthDate,
                                          RemainingPoint = 0,
                                          TierName = t.Name
                                      };


                    if (!customerQry.Any())
                    {
                        return new CustomerInfoResult() { IsSuccess = false, Message = "Not found" };
                    }

                    CustomerDto foundCustomer = customerQry.First();
                    int remainingPoint = 0;

                    if (includeRemainingAmount)
                    {
                        remainingPoint = context.Transactions.Where(t => t.CustomerId == foundCustomer.ID).Sum(t => t.Point);
                    }

                    foundCustomer.RemainingPoint = remainingPoint;


                    return new CustomerInfoResult() { IsSuccess = true, Customer = foundCustomer };
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        public static RedeemResultDto Redeem(string code, int redeemAmount)
        {

            using (JiewStoreEntities context = new JiewStoreEntities())
            {
                try
                {
                    var userQuery = context.Customers.Where(c => c.Code == code);

                    if (!userQuery.Any())
                    {
                        return new RedeemResultDto() { IsSuccess = false, Message = $"Customer code [{code}] was not found", Code = code, Point = redeemAmount };
                    }

                    var customer = userQuery.First();

                    var tranQuery = context.Transactions.Where(t => t.CustomerId == customer.ID);

                    int remainPoint = tranQuery.Sum(t => t.Point);

                    if (remainPoint < redeemAmount)
                    {
                        return new RedeemResultDto() { IsSuccess = false, Message = $"Redeem amount is exceed the remaining = {remainPoint}", Code = code, Point = redeemAmount };
                    }

                    Transaction newTransaction = TransactionHelper.MakeNewRedeemPointTransaction(customer, redeemAmount);

                    context.Transactions.Add(newTransaction);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }

            return new RedeemResultDto() { IsSuccess = true, Code = code, Point = redeemAmount };
        }

        public static AddBuyAmountResultDto AddBuyAmount(string code, float amount)
        {

            float pointPerAmount = 50f;
            float pointMultipler = 1f;            

            if (amount <= 0)
            {
                return new AddBuyAmountResultDto() { IsSuccess = false, Message = $"Amount is less than minimum amount[{pointPerAmount}]", Code = code, Amount = amount };
            }
      

            using (JiewStoreEntities context = new JiewStoreEntities())
            {
                try
                {
                    var userQuery = context.Customers.Where(c => c.Code == code);

                    if (!userQuery.Any())
                    {
                        return new AddBuyAmountResultDto() { IsSuccess = false, Message = $"Customer code [{code}] was not found", Code = code, Amount = amount };
                    }

                    var customer = userQuery.First();

                    pointMultipler = BirthMonthBonusPointCondition.GetMultipler(customer.BirthDate);

                    IPointCalculator pointCalculator = new JiewPointCalculator(pointPerAmount, pointMultipler);

                    Transaction newTransaction = TransactionHelper.MakeNewAmountTransaction(customer,
                        amount,
                        pointPerAmount,
                        pointMultipler,
                        "buy",
                        pointCalculator);

                    context.Transactions.Add(newTransaction);
                    context.SaveChanges();

                    CustomerDto customerDto = AutoMapper.Mapper.Map<CustomerDto>(customer);

                    customerDto.RemainingPoint = context.Transactions.Where(t => t.CustomerId == customer.ID).Sum(t => t.Point);

                    return new AddBuyAmountResultDto() { IsSuccess = true, Amount = amount, Code = code, Customer = customerDto, Message = "" };
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }

        }

        public static CustomerDto[] GetCustomers(int pageNo, int pageSize)
        {
            if (pageNo <= 0)
            {
                throw new Exception("Invalid pageNo");
            }

            if (pageSize <= 0)
            {
                throw new Exception("Invalid pageSize");
            }

            //linq paging
            //https://stackoverflow.com/questions/548475/efficient-way-to-implement-paging
            using (JiewStoreEntities context = new JiewStoreEntities())
            {

                int skipCount = (pageNo - 1) * pageSize;

                //need order before skip
                //https://stackoverflow.com/questions/3437178/the-method-skip-is-only-supported-for-sorted-input-in-linq-to-entities
                //var qry = (from c in context.Customers select c).OrderBy(c=> c.ID).Skip(skipCount).Take(pageSize);
                //return AutoMapper.Mapper.Map<CustomerDto[]>(qry.ToArray()); ;

                var qry = from c in context.Customers
                          join t in context.Transactions
                          on c.ID equals t.CustomerId
                          join l in context.CustomerTiers
                          on c.CustomerTierID equals l.ID
                          group new { c, t, l } by new { c.ID }
                          into ct
                          let c = ct.FirstOrDefault().c
                          let l = ct.FirstOrDefault().l
                          select new CustomerDto
                          {
                              ID = c.ID,
                              Code = c.Code,
                              NickName = c.NickName,
                              FullName = c.FullName,
                              Facebook = c.Facebook,
                              Phone = c.Phone,
                              Address = c.Address,
                              BirthDate = c.BirthDate,
                              RemainingPoint = ct.Sum(x => x.t.Point),
                              TierName = l.Name
                          };

                qry = qry.OrderBy(c => c.ID).Skip(skipCount).Take(pageSize);

                return qry.ToArray();

            }
        }
        
    }
}