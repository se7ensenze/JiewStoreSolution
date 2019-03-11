using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace JiewStore.Controllers
{
    public class ParameterController : ApiController
    {
        [HttpPost]
        [Route("parameter/getParameters")]
        public async Task<IHttpActionResult> GetParameters()
        {
            var result = await Task.Run(() =>
            {
                return ParameterFacade.GetParameters();
            });

            return this.Ok(result);
        }
    }
}
