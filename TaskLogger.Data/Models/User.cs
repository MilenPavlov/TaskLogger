using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TaskLogger.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            UserType = UserType.User;
        }
        public string UserId => Id;

        public string DisplayName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string Tag { get; set; }
        public string Gender { get; set; }

        public bool Archived { get; set; }

        public UserType UserType { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here

            return userIdentity;
        }
    }
}