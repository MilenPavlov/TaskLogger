using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLogger.Data.Models;

namespace TaskLogger.Data.Abstract
{
    public interface IUnitOfWork
    {
        //GenericRepository<User> UseRepository { get; }
        GenericRepository<UserTask> UserTaskRepository { get; }

        GenericRepository<UserTaskEntry> UserTaskEntryRepository { get; }

        void Save();

        Task SaveAsync();
        void Dispose(bool disposing);

        void Dispose();
    }
}
