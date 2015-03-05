using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TaskLogger.Data.Models;

namespace TaskLogger.Data.Concrete
{
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    using TaskLogger.Data.Context;
    using TaskLogger.Data.Services;

    public class TaskLoggerUserManager : UserManager<User>
    {
        public TaskLoggerUserManager(IUserStore<User> store) : base(store)
        {
        }

        //public TaskLoggerUserManager()
        //    : this(new TaskLoggerUserStore())
        //{
        //}

        public static TaskLoggerUserManager Create(
            IdentityFactoryOptions<TaskLoggerUserManager> options,
            IOwinContext context)
        {
            var dbContext = context.Get<TaskLoggerContext>();
            var userManager = new TaskLoggerUserManager(new UserStore<User>(dbContext));

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                userManager.UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }

            return userManager;
        }
    }
}
