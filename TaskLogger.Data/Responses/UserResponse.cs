using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLogger.Data.Models;

namespace TaskLogger.Data.Responses
{
    public class UserResponse : ResponseBase
    {
        public User User { get; set; }
        public UserImage UserImage { get; set; }
    }
}
