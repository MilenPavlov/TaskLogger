using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TaskLogger.Data.Models
{
    public class User : IdentityUser
    {     
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public int Age { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
