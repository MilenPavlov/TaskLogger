using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLogger.Data.Responses
{
    using TaskLogger.Data.Models;

    public class UserTaskResponse : ResponseBase
    {
        public UserTask UserTask { get; set; }
    }
}
