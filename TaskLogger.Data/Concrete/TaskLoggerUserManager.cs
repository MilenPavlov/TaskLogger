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
    public class TaskLoggerUserManager : UserManager<User>
    {
        public TaskLoggerUserManager(IUserStore<User> store) : base(store)
        {
        }

        public TaskLoggerUserManager() : this(new TaskLoggerUserStore())
        {         
        }
    }
}
