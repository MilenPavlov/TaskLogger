using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TaskLogger.Data.Abstract;
using TaskLogger.Data.Context;
using TaskLogger.Data.Models;

namespace TaskLogger.Data.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskLoggerUserStore _store;
        private readonly TaskLoggerUserManager _manager;

        public UserRepository()
        {
            _store = new TaskLoggerUserStore(new TaskLoggerContext());
            _manager = new TaskLoggerUserManager(_store);
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (!_disposed)
            {
                _store.Dispose();
                _manager.Dispose();
            }

            _disposed = true;
        }

        public async Task<User> GetUserByNameAsync(string username)
        {
            return await _store.FindByNameAsync(username);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _store.Users.ToArrayAsync();
        }

        public async Task CreateAsync(User user, string password)
        {
            await _manager.CreateAsync(user, password);
        }

        public async Task DeleteAsync(User user)
        {
            var result = await _manager.DeleteAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            var result = await _manager.UpdateAsync(user);
        }

        public bool VerifyUserPassword(string hashedPassword, string providedPassword)
        {
            return _manager.PasswordHasher.VerifyHashedPassword(hashedPassword, providedPassword) ==
                   PasswordVerificationResult.Success;
        }

        public string HashPassword(string password)
        {
            return _manager.PasswordHasher.HashPassword(password);
        }

        public async Task AddUserToRoleAsync(User newUser, string role)
        {
            await _manager.AddToRoleAsync(newUser.Id, role);
        }

        public async Task<IEnumerable<string>> GetRolesForUserAsync(User user)
        {
            return await _manager.GetRolesAsync(user.Id);
        }

        public async Task RemoveUserFromRoleAsync(User user, params string[] roleNames)
        {
            await _manager.RemoveFromRolesAsync(user.Id, roleNames);
        }

        public async Task<User> GetLoginUserAsync(string userName, string password)
        {
            return await _manager.FindAsync(userName, password);
        }

        public async Task<ClaimsIdentity> CreateIdentityAsync(User user)
        {
            return await _manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
