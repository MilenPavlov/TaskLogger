using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TaskLogger.Data.Models;

namespace TaskLogger.Data.Abstract
{
    public interface IUserRepository
    {
        User GetUser(string UserName, string Password);

        IEnumerable<User> GetUsers();

        string AddUser(User user);

        string EditUser(User user);

        string DeleteUser(User user);
    }
}
