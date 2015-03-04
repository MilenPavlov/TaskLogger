using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TaskLogger.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using TaskLogger.Data.Concrete;

    public class BaseApiController : ApiController
    {
        private TaskLoggerUserManager _UserManager = null;

        protected TaskLoggerUserManager UserManager
        {
            get
            {
                return _UserManager ?? Request.GetOwinContext().GetUserManager<TaskLoggerUserManager>();
            }
        }

        public BaseApiController()
        {}

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                            ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return this.BadRequest();
                }

                return this.BadRequest();
            }

            return null;
        }
    }
}
