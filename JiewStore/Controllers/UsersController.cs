using JiewStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace JiewStore.Controllers
{
    public class UsersController : ApiController
    {

        //IHttpActionResult
        //https://stackoverflow.com/questions/23196931/returning-ihttpactionresult-vs-ienumerableitem-vs-iqueryableitem/25374159

        [Route("users/getUsers")]
        public async Task<IHttpActionResult> GetUsers()
        {
            User[] result = await Task.Run(() => {
                return new User[] {
                    new User() { Name = "Tos", Age = 33 },
                    new User() { Name = "Jiew", Age = 31 },
                    new User() { Name = "Tah", Age = 28 }
                };
            });
            return this.Ok(result);
        }
    }
}
