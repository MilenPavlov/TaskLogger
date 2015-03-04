using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TaskLogger.Data.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;

    public class User : IdentityUser
    {     
        public string DisplayName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here

            return userIdentity;
        }
    }
}
