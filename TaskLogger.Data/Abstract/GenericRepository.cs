﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using System.Threading.Tasks;
using TaskLogger.Data.Context;
using TaskLogger.Data.Models;

namespace TaskLogger.Data.Abstract
{
    public class  GenericRepository<T> where T : class 
    {
        private readonly TaskLoggerContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(TaskLoggerContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual IList<T> Get(Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual async Task<IList<T>> GetAsync(Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual T GetByID(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<bool> ExistsAsync(object id)
        {
            return await _dbSet.AnyAsync(x => x.Equals(id));
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }


        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual async Task InsertAsync(T entity)
        {
            _dbSet.Add(entity);
        }


        public virtual void Delete(object id)
        {
            T entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual async Task DeleteAsync(object id)
        {
            T entityToDelete = await _dbSet.FindAsync(id);
            Delete(entityToDelete);
        }


        public virtual void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual async Task UpdateAsync(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
