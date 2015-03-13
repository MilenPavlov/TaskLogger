using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TaskLogger.Controllers
{
    using System.Threading.Tasks;

    using TaskLogger.Data.Abstract;

    [RoutePrefix("usertaskentry")]
    public class UserTaskEntryController : BaseApiController
    {
        private readonly IUnitOfWork _uow;

        public UserTaskEntryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [Route("all/{userId:string}")]
        public async Task<IHttpActionResult> GetAllEntries(string userId)
        {
            try
            {

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}
