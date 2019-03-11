using JiewStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace JiewStore.Controllers
{
    public class CustomersController : ApiController
    {
        [HttpPost]
        [Route("customers/addNewCustomer")]
        public async Task<IHttpActionResult> AddNewCustomer(NewCustomerInfo newCustomer)
        {
            var result =  await Task.Run(() => {
                return CustomerFacade.NewCustomer(newCustomer.NickName, 
                    newCustomer.FullName,
                    newCustomer.Facebook, 
                    newCustomer.Phone, 
                    newCustomer.BirthDate, 
                    newCustomer.Address, 
                    newCustomer.Amount);
             });
            return this.Ok(result);
        }

        [HttpPost]
        [Route("customers/getCustomers")]
        public async Task<IHttpActionResult> GetCustomers(GetCustomerPagingInfo info)
        {
            var result = await Task.Run(() => {
                return CustomerFacade.GetCustomers(info.PageNo, info.PageSize);
            });
            return this.Ok(result);
        }

        [HttpPost]
        [Route("customers/redeem")]
        public async Task<IHttpActionResult> Redeem(RedeemRequest request)
        {
            var result = await Task.Run(() => {
                return CustomerFacade.Redeem(request.Code, request.Amount);
            });
            return this.Ok(result);
        }

        [HttpPost]
        [Route("customers/addBuyAmount")]
        public async Task<IHttpActionResult> AddBuyAmount(RedeemRequest request)
        {
            var result = await Task.Run(() => {
                return CustomerFacade.AddBuyAmount(request.Code, request.Amount);
            });
            return this.Ok(result);
        }

        [HttpPost]
        [Route("customers/getCustomerInfo")]
        public async Task<IHttpActionResult> GetCustomerInfo(GetCustomerInfo request)
        {
            var result = await Task.Run(() => {
                return CustomerFacade.GetCustomer(request.Code, request.IncludeRemainingPoint);
            });
            return this.Ok(result);
        }
    }
}
