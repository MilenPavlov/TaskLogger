using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using TaskLogger.Data.Context;
using TaskLogger.Data.Models;

namespace TaskLogger.Data.Concrete
{
    public class TaskLoggerUserStore : UserStore<User>
    {
        public TaskLoggerUserStore(TaskLoggerContext context): base(context)
        {        
        }

        public TaskLoggerUserStore()
            : this(new TaskLoggerContext())
        {
        }
    }
}
