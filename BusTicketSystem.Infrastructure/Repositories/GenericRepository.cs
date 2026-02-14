using BusTicketSystem.Application.Interfaces.Repositories;
using BusTicketSystem.Domain.Common;
using BusTicketSystem.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusTicketSystem.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> AddAsync(T entity)
        {
            //ef core state tracking
            await _dbSet.AddAsync(entity);
            return true;
        }
        
        public async Task<List<T>> GetAllAsync()
        {
            //AsNoTracking() -> sadece okuma yaparken performans artırır(cachleme yapmaz)
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if(includes != null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> GetWhereAsync(Expression<Func< T, bool>> expression)
        {
            return await _dbSet.Where(expression).AsNoTracking().ToListAsync();
        }

        public bool Remove(T entity)
        {
            _dbSet.Remove(entity);
            return true;
        }

        public bool Update(T entity)
        {
            _dbSet.Update(entity);
            return true;
        }
    }
}
