using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TaskLogger.Data.Models;

namespace TaskLogger.Data.Abstract
{
    public interface IUserRepository : IDisposable
    {
        Task<User> GetUserByNameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task CreateAsync(User user, string password);
        Task DeleteAsync(User user);
        Task UpdateAsync(User user);

        bool VerifyUserPassword(string hashedPassword, string providedPassword);
        string HashPassword(string password);

        Task AddUserToRoleAsync(User newUser, string role);

        Task<IEnumerable<string>> GetRolesForUserAsync(User user);

        Task RemoveUserFromRoleAsync(User user, params string[] roleNames);

        Task<User> GetLoginUserAsync(string userName, string password);

        Task<ClaimsIdentity> CreateIdentityAsync(User user);
    }
}
   