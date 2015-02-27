using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLogger.Data.Abstract;
using TaskLogger.Data.Context;
using TaskLogger.Data.Models;

namespace TaskLogger.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private TaskLoggerContext _context = new TaskLoggerContext();
        private GenericRepository<User> _userRepository;
        private GenericRepository<UserTask> _userTaskRepository;
        private GenericRepository<UserTaskEntry> _userTaskEntryRepository;


        public GenericRepository<User> UseRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new GenericRepository<User>(_context);
                }

                return _userRepository;
            }
        }

        public GenericRepository<UserTask> UserTaskRepository
        {
            get
            {
                if (_userTaskRepository == null)
                {
                    _userTaskRepository = new GenericRepository<UserTask>(_context);
                }

                return _userTaskRepository;
            }
        }

        public GenericRepository<UserTaskEntry> UserTaskEntryRepository
        {
            get
            {
                if (_userTaskEntryRepository == null)
                {
                    _userTaskEntryRepository = new GenericRepository<UserTaskEntry>(_context);
                }

                return _userTaskEntryRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
