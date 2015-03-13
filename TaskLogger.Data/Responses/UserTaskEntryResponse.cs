using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLogger.Data.Responses
{
    using TaskLogger.Data.Models;

    public class UserTaskEntryResponse : ResponseBase
    {
        public UserTaskEntry UserTaskEntry { get; set; }
    }
}
